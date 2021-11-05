import 'package:autoshop_application/blocs/operacao/bloc.dart';
import 'package:autoshop_application/constants/colors.dart';
import 'package:autoshop_application/models/models.dart';
import 'package:autoshop_application/ui/screens/operacao/form.dart';
import 'package:autoshop_application/ui/widgets/app_custom_drawer.dart';
import 'package:autoshop_application/ui/widgets/shared/error.dart';
import 'package:autoshop_application/ui/widgets/shared/noresult_found.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class OperacaoList extends StatefulWidget {
  const OperacaoList({ Key? key }) : super(key: key);

  @override
  _OperacaoListState createState() => _OperacaoListState();
}

class _OperacaoListState extends State<OperacaoList> {
  @override
  void initState() {
    super.initState();
    BlocProvider.of<OperacaoBloc>(context).add(GetAllOperacoesEvent());
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(),
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          Navigator.push(context,
              MaterialPageRoute(builder: (BuildContext context) {
            return const OperacaoForm(newOperacao: true);
          }));
        },
        child: const Icon(Icons.add),
        backgroundColor: AppColor.headerBarColor,
      ),
      drawer: const AppCustomDrawer(),
      body: Center(child: BlocBuilder<OperacaoBloc, OperacaoState>(
          builder: (BuildContext context, OperacaoState state) {
        if (state is LoadingState) {
          return const Center(
            child: CircularProgressIndicator(),
          );
        }
        if (state is ErrorState) {
          return Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              CustomError(customMessage: state.message),
              GestureDetector(
                  onTap: () {
                    BlocProvider.of<OperacaoBloc>(context)
                        .add(GetAllOperacoesEvent());
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
                        TextSpan(text: 'para recarregar a lista de operações'),
                      ],
                    ),
                  ))
            ],
          );
        }
        if (state is LoadedSucessState) {
          if (state.operacoes.isEmpty) {
            return const NoResultFound(
                customMessage: "Nenhuma operação cadastrada");
          }
          return RefreshIndicator(
            onRefresh: () {
              return Future.delayed(const Duration(seconds: 1), () {
                BlocProvider.of<OperacaoBloc>(context)
                    .add(GetAllOperacoesEvent());
              });
            },
            child: ListView.builder(
              itemCount: state.operacoes.length,
              itemBuilder: (context, index) {
                final item = state.operacoes[index];
                return _operacaoCard(item, context);
              },
              physics: const AlwaysScrollableScrollPhysics(),
            ),
          );
        }
        return Container();
      })),
    );
  }

  InkWell _operacaoCard(Operacao operacao, BuildContext context) {
    return InkWell(
      onTap: () => {
        //Navigator.push(context,
        //    MaterialPageRoute(builder: (BuildContext context) {
        //  return OperacaoDetails(newOperacao: false, Operacao: Operacao);
        //}))
      },
      child: Card(
        color: AppColor.vehicleCardColor,
        elevation: 8.0,
        margin: const EdgeInsets.symmetric(horizontal: 10.0, vertical: 6.0),
        child: Row(
          children: [
            Flexible(
              child: Container(
                child: _operacaoListTitle(operacao, context),
              ),
            ),
            //IconButton(
            //  onPressed: () => {
            //    Navigator.push(context,
            //        MaterialPageRoute(builder: (BuildContext context) {
            //      return OperacaoForm(newOperacao: false, Operacao: operacao);
            //    }))
            //  },
            //  icon: const Icon(Icons.edit, color: Colors.yellow),
            //)
          ],
        ),
      ),
    );
  }

  ListTile _operacaoListTitle(Operacao Operacao, BuildContext context) {
    return ListTile(
        contentPadding:
            const EdgeInsets.symmetric(horizontal: 20.0, vertical: 5.0),
        title: Text(
          Operacao.id!,
          style: const TextStyle(
              color: Colors.white, fontWeight: FontWeight.bold, fontSize: 20),
        ),
        subtitle: Row(
          children: <Widget>[
            Text(Operacao.valorTotal.toString(),
                style: const TextStyle(color: Colors.white, fontSize: 20))
          ],
        ));
  }

  @override
  void dispose() {
    super.dispose();
  }

}