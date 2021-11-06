import 'package:autoshop_application/blocs/produto/bloc.dart';
import 'package:autoshop_application/constants/colors.dart';
import 'package:autoshop_application/enums/produto_tipo_enum.dart';
import 'package:autoshop_application/models/models.dart';
import 'package:autoshop_application/ui/screens/produto/shared/fields.dart';
import 'package:autoshop_application/ui/widgets/shared/app_select_input.dart';
import 'package:enum_to_string/enum_to_string.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class ProdutoForm extends StatefulWidget {
  final bool newProduto;
  final Produto? produto;
  final int? index;

  const ProdutoForm(
      {Key? key, required this.newProduto, this.produto, this.index})
      : super(key: key);

  @override
  _ProdutoFormState createState() => _ProdutoFormState();
}

class _ProdutoFormState extends State<ProdutoForm> {
  final _formKey = GlobalKey<FormState>();
  final Map<String, dynamic> _formData = {};

  @override
  Widget build(BuildContext context) {
    _loadFormData();

    return BlocListener(
      bloc: BlocProvider.of<ProdutoBloc>(context),
      listener: (BuildContext context, ProdutoState state) {
        if (state is ProdutoErrorState) {
          _showErrorDialog(state);
        }
        if (state is ProdutoLoadedSucessState) {
          Navigator.pop(context);
        }
      },
      child: Scaffold(
        appBar: AppBar(
          title: Text(
            widget.newProduto ? 'Cadastrar' : 'Editar',
            style: const TextStyle(color: Colors.white),
          ),
          leading: IconButton(
            icon: const Icon(
              Icons.arrow_back,
              color: Colors.white,
            ),
            onPressed: () {
              ProdutoBloc bloc = BlocProvider.of<ProdutoBloc>(context);
              if(bloc.state is ProdutoErrorState){
                BlocProvider.of<ProdutoBloc>(context).add(GetAllProdutosEvent());
              }
              Navigator.pop(context);            
            },
          ),
        ),
        floatingActionButton: Padding(
            padding: const EdgeInsets.only(left: 10, bottom: 10),
            child: FloatingActionButton.extended(
                backgroundColor: AppColor.headerBarColor,
                icon: const Icon(Icons.add),
                label: Text(widget.newProduto ? 'Adicionar' : 'Atualizar'),
                onPressed: () {
                  if (_formKey.currentState!.validate()) {
                    widget.newProduto
                        ? BlocProvider.of<ProdutoBloc>(context).add(
                            CreateProdutoEvent(
                                Produto.jsonMapInsert(_formData)))
                        : BlocProvider.of<ProdutoBloc>(context).add(
                            UpdateProdutoEvent(
                                Produto.jsonMapUpdate(_formData)));
                  }
                })),
        body: SingleChildScrollView(
          child: Form(
            key: _formKey,
            child: Padding(
                padding: const EdgeInsets.only(top: 30, left: 10, right: 10),
                child: Column(
                  children: [
                    ProdutoNomeField(formData: _formData),
                    const SizedBox(
                      height: 20,
                    ),
                    ProdutoPrecoField(formData: _formData),
                    const SizedBox(
                      height: 20,
                    ),
                    AppSelectInput<String>(
                      hintText: "Tipo",
                      options: EnumToString.toList(ProdutoTipoEnum.values),
                      values: EnumToString.toList(ProdutoTipoEnum.values),
                      formData: _formData,
                      formProperty: "tipo",
                    )
                  ],
                )),
          ),
        ),
      ),
    );
  }

  void _loadFormData() {
    _formData["isCreate"] = widget.newProduto;
    if (!widget.newProduto) {
      Produto produtoAtual = widget.produto!;
      _formData['id'] = produtoAtual.id;
      _formData['nome'] = produtoAtual.nome;
      _formData['valor'] = "R\$ " + produtoAtual.preco.toString();
      _formData['tipo'] = EnumToString.convertToString(produtoAtual.tipo);
    } else {
      _formData['nome'] = "";
      _formData['valor'] = "";
      _formData['tipo'] = EnumToString.convertToString(ProdutoTipoEnum.Acessorio);
    }
  }

  void _showErrorDialog(ProdutoErrorState errorState) {
    showDialog(
        context: context,
        builder: (BuildContext context) {
          return AlertDialog(
            title: const Text('Erro no cadastro'),
            content: Text(errorState.message),
            actions: <Widget>[
              TextButton(
                onPressed: () => Navigator.pop(context, 'OK'),
                child: const Text('OK'),
              ),
            ],
          );
        });
  }
}
