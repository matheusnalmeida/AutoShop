import 'package:autoshop_application/blocs/produto/bloc.dart';
import 'package:autoshop_application/constants/colors.dart';
import 'package:autoshop_application/exceptions/invalid_state_exception.dart';
import 'package:autoshop_application/models/models.dart';
import 'package:autoshop_application/ui/screens/produto/details.dart';
import 'package:autoshop_application/ui/screens/produto/form.dart';
import 'package:autoshop_application/ui/widgets/app_custom_drawer.dart';
import 'package:autoshop_application/ui/widgets/shared/error.dart';
import 'package:autoshop_application/ui/widgets/shared/noresult_found.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class ProdutoList extends StatefulWidget {
  const ProdutoList({ Key? key }) : super(key: key);

  @override
  _ProdutoListState createState() => _ProdutoListState();
}

class _ProdutoListState extends State<ProdutoList> {
  @override
  void initState() {
    super.initState();
    BlocProvider.of<ProdutoBloc>(context).add(GetAllProdutosEvent());
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text("Produtos")),
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          Navigator.push(context,
              MaterialPageRoute(builder: (BuildContext context) {
            return const ProdutoForm(newProduto: true);
          }));
        },
        child: const Icon(Icons.add),
        backgroundColor: AppColor.headerBarColor,
      ),
      drawer: const AppCustomDrawer(),
      body: Center(child: BlocBuilder<ProdutoBloc, ProdutoState>(
          builder: (BuildContext context, ProdutoState state) {
        if (state is ProdutoLoadingState) {
          return const Center(
            child: CircularProgressIndicator(),
          );
        }
        if (state is ProdutoErrorState) {
          return Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              CustomError(customMessage: state.message),
              GestureDetector(
                  onTap: () {
                    BlocProvider.of<ProdutoBloc>(context)
                        .add(GetAllProdutosEvent());
                  },
                  child: RichText(
                    text: const TextSpan(
                      style: TextStyle(
                        fontSize: 14.0,
                        color: Colors.black,
                      ),
                      children: <TextSpan>[
                        TextSpan(text: 'Clique'),
                        TextSpan(
                            text: ' Aqui ',
                            style: TextStyle(
                                fontWeight: FontWeight.bold,
                                color: Colors.blue)),
                        TextSpan(text: 'para recarregar a lista de produtos'),
                      ],
                    ),
                  ))
            ],
          );
        }
        if (state is ProdutoLoadedSucessState) {
          if (state.produtos.isEmpty) {
            return const NoResultFound(
                customMessage: "Nenhum produto cadastrado");
          }
          return RefreshIndicator(
            onRefresh: () {
              return Future.delayed(const Duration(seconds: 1), () {
                BlocProvider.of<ProdutoBloc>(context)
                    .add(GetAllProdutosEvent());
              });
            },
            child: ListView.builder(
              itemCount: state.produtos.length,
              itemBuilder: (context, index) {
                final item = state.produtos[index];
                return _productCard(item, context);
              },
              physics: const AlwaysScrollableScrollPhysics(),
            ),
          );
        }
        throw InvalidStateException("Estado de listagem dos produtos é inválido!");
      })),
    );
  }

  InkWell _productCard(Produto produto, BuildContext context) {
    return InkWell(
      onTap: () => {
        Navigator.push(context,
            MaterialPageRoute(builder: (BuildContext context) {
          return ProdutoDetails(newProduto: false, produto: produto);
        }))
      },
      child: Card(
        color: AppColor.vehicleCardColor,
        elevation: 8.0,
        margin: const EdgeInsets.symmetric(horizontal: 10.0, vertical: 6.0),
        child: Row(
          children: [
            Flexible(
              child: Container(
                child: _productListTitle(produto, context),
              ),
            ),
            IconButton(
              onPressed: () => {
                Navigator.push(context,
                    MaterialPageRoute(builder: (BuildContext context) {
                  return ProdutoForm(newProduto: false, produto: produto);
                }))
              },
              icon: const Icon(Icons.edit, color: Colors.yellow),
            ),
            IconButton(
                onPressed: () =>
                    {_showDeleteDialog(DeleteProdutoEvent(produto))},
                icon: const Icon(Icons.delete, color: Colors.red)),
          ],
        ),
      ),
    );
  }

  ListTile _productListTitle(Produto produto, BuildContext context) {
    return ListTile(
        contentPadding:
            const EdgeInsets.symmetric(horizontal: 20.0, vertical: 5.0),
        title: Text(
          produto.nome!,
          style: const TextStyle(
              color: Colors.white, fontWeight: FontWeight.bold, fontSize: 20),
        ),
        subtitle: Row(
          children: <Widget>[
            Text(produto.preco.toString(),
                style: const TextStyle(color: Colors.white, fontSize: 20))
          ],
        ));
  }

  @override
  void dispose() {
    super.dispose();
  }

  void _showDeleteDialog(DeleteProdutoEvent event) {
    showDialog(
        context: context,
        builder: (BuildContext context) {
          return AlertDialog(
            title: const Text('Confirmar Deleção'),
            content: const Text("Deseja realmente deletar este produto?"),
            actions: <Widget>[
              TextButton(
                onPressed: () {
                  Navigator.pop(context, 'OK');
                  BlocProvider.of<ProdutoBloc>(context).add(event);
                },
                child: const Text('Sim'),
              ),
              TextButton(
                onPressed: () => Navigator.pop(context, 'OK'),
                child: const Text('Não'),
              ),
            ],
          );
        });
  }
}