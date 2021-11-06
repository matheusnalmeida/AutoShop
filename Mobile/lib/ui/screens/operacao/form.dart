import 'package:autoshop_application/blocs/operacao/operacao_bloc.dart';
import 'package:autoshop_application/blocs/produto/bloc.dart';
import 'package:autoshop_application/blocs/veiculo/bloc.dart';
import 'package:autoshop_application/constants/colors.dart';
import 'package:autoshop_application/enums/veiculo_tipo_enum.dart';
import 'package:autoshop_application/exceptions/invalid_state_exception.dart';
import 'package:autoshop_application/models/models.dart';
import 'package:autoshop_application/ui/screens/operacao/shared/fields.dart';
import 'package:autoshop_application/ui/widgets/shared/app_select_input.dart';
import 'package:enum_to_string/enum_to_string.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:multi_select_flutter/multi_select_flutter.dart';

class OperacaoForm extends StatefulWidget {
  final bool newOperacao;
  final Operacao? operacao;
  final int? index;

  const OperacaoForm(
      {Key? key, required this.newOperacao, this.operacao, this.index})
      : super(key: key);

  @override
  _OperacaoFormState createState() => _OperacaoFormState();
}

class _OperacaoFormState extends State<OperacaoForm> {
  final _formKey = GlobalKey<FormState>();
  final _formFieldKey = GlobalKey<FormFieldState>();
  final Map<String, dynamic> _formData = {};

  @override
  void initState() {
    super.initState();
    BlocProvider.of<ProdutoBloc>(context).add(GetAllProdutosEvent());
    BlocProvider.of<VeiculoBloc>(context).add(GetAllVeiculosEvent());
  }

  @override
  Widget build(BuildContext context) {
    _loadFormData();

    return BlocListener(
      bloc: BlocProvider.of<OperacaoBloc>(context),
      listener: (BuildContext context, OperacaoState state) {
        if (state is OperacaoErrorState) {
          _showErrorDialog(state);
        }
        if (state is OperacaoLoadedSucessState) {
          Navigator.pop(context);
        }
      },
      child: Scaffold(
        appBar: AppBar(
          title: Text(
            widget.newOperacao ? 'Cadastrar' : 'Editar',
            style: const TextStyle(color: Colors.white),
          ),
          leading: IconButton(
            icon: const Icon(
              Icons.arrow_back,
              color: Colors.white,
            ),
            onPressed: () {
              OperacaoBloc bloc = BlocProvider.of<OperacaoBloc>(context);
              if (bloc.state is OperacaoErrorState) {
                BlocProvider.of<OperacaoBloc>(context)
                    .add(GetAllOperacoesEvent());
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
                label: Text(widget.newOperacao ? 'Adicionar' : 'Atualizar'),
                onPressed: () {
                  if (_formValidState()) {
                    widget.newOperacao
                        ? BlocProvider.of<OperacaoBloc>(context).add(
                            CreateOperacaoEvent(
                                OperacaoCreate.jsonMapInsert(_formData)))
                        : BlocProvider.of<OperacaoBloc>(context).add(
                            UpdateOperacaoEvent(
                                Operacao.jsonMapUpdate(_formData)));
                  }
                })),
        body: SingleChildScrollView(
          child: Form(
            key: _formKey,
            child: Padding(
                padding: const EdgeInsets.only(top: 30, left: 10, right: 10),
                child: Column(
                  children: [
                    OperacaoQuantidadeParcelasField(formData: _formData),
                    const SizedBox(
                      height: 20,
                    ),
                    Center(child: BlocBuilder<VeiculoBloc, VeiculoState>(
                        builder: (BuildContext context, VeiculoState state) {
                      if (state is VeiculoLoadingState) {
                        return const CircularProgressIndicator();
                      } else if (state is VeiculoLoadedSucessState) {
                        return AppSelectInput<String>(
                          hintText: "Veiculo",
                          options: state.veiculos
                              .map<String>((veiculo) =>
                                  "${veiculo.nome!} - R\$ ${veiculo.preco!}")
                              .toList(),
                          values: state.veiculos
                              .map<String>((veiculo) => veiculo.id!)
                              .toList(),
                          formData: _formData,
                          formProperty: "veiculo",
                        );
                      }
                      throw InvalidStateException(
                          "Estado de listagem dos veiculos é inválido!");
                    })),
                    const SizedBox(
                      height: 20,
                    ),
                    Center(child: BlocBuilder<ProdutoBloc, ProdutoState>(
                        builder: (BuildContext context, ProdutoState state) {
                      if (state is ProdutoLoadingState) {
                        return const CircularProgressIndicator();
                      } else if (state is ProdutoLoadedSucessState) {
                        return Container(
                          padding: const EdgeInsets.only(
                              top: 10.0, bottom: 10.0, left: 5),
                          decoration: BoxDecoration(
                              border:
                                  Border.all(color: const Color(0x61000000))),
                          child: MultiSelectDialogField<String?>(
                            title: const Text("Selecione"),
                            decoration: const BoxDecoration(border: null),
                            buttonText: const Text(
                              "Produtos",
                              style: TextStyle(
                                  color: Color(0x99000000), fontSize: 22),
                            ),
                            backgroundColor: AppColor.backgroundColor,
                            buttonIcon: const Icon(Icons.add),
                            items: state.produtos
                                .map((produto) => MultiSelectItem(produto.id, "${produto.nome!} - R\$ ${produto.preco!}"))
                                .toList(),
                            chipDisplay: MultiSelectChipDisplay(
                              chipColor: AppColor.headerBarColor,
                              textStyle: const TextStyle(
                                  color: Colors.black, fontSize: 17),
                            ),
                            //key: _formFieldKey,
                            //validator: (value) {
                            //  if (value == null || value.isEmpty) {
                            //    return "Selecione ao menos um produto";
                            //  }
                            //  return null;
                            //},
                            onConfirm: (values) {
                              _formData["Produtos"] = values;
                              //_formFieldKey.currentState!.validate();
                            },
                          ),
                        );
                      }
                      throw InvalidStateException(
                          "Estado de listagem dos produtos é inválido!");
                    }))
                  ],
                )),
          ),
        ),
      ),
    );
  }

  void _loadFormData() {
    _formData["isCreate"] = widget.newOperacao;
    if (!widget.newOperacao) {
      Operacao operacaoAtual = widget.operacao!;
    } else {
      _formData['quantidadeParcelas'] = "";
      _formData["Produtos"] = [];
    }
  }

  bool _formValidState() {
    var generalFieldsValid = _formKey.currentState!.validate();
    var produtosFormFieldValid = _formFieldKey.currentState!.validate();
    return generalFieldsValid && produtosFormFieldValid;
  }

  void _showErrorDialog(OperacaoErrorState errorState) {
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
