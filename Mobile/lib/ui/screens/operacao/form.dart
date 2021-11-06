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
    BlocProvider.of<OperacaoBloc>(context).add(GetOperacaoCreateEvent());
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
        if (state is OperacaoLoadedErrorCreateState) {
          _showLoadErrorDialog(state);
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
              if (bloc.state is! GetAllOperacoesEvent) {
                BlocProvider.of<OperacaoBloc>(context)
                    .add(GetAllOperacoesEvent());
              }
            },
          ),
        ),
        floatingActionButton: Padding(
            padding: const EdgeInsets.only(left: 10, bottom: 10),
            child: BlocBuilder<OperacaoBloc, OperacaoState>(
                builder: (BuildContext context, OperacaoState state) {
              return FloatingActionButton.extended(
                  backgroundColor: _shouldEnableAddButton(state) ? AppColor.headerBarColor : Colors.grey,
                  icon: const Icon(Icons.add),
                  label: Text(widget.newOperacao ? 'Adicionar' : 'Atualizar'),
                  onPressed: () {
                    if(!_shouldEnableAddButton(state)){
                      return;
                    }
                    if (_formValidState()) {
                      widget.newOperacao
                          ? BlocProvider.of<OperacaoBloc>(context).add(
                              CreateOperacaoEvent(
                                  OperacaoCreate.jsonMapInsert(_formData)))
                          : BlocProvider.of<OperacaoBloc>(context).add(
                              UpdateOperacaoEvent(
                                  Operacao.jsonMapUpdate(_formData)));
                    }
                  });
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
                    BlocBuilder<OperacaoBloc, OperacaoState>(
                        builder: (BuildContext context, OperacaoState state) {
                      if (state is OperacaoLoadingState) {
                        return const CircularProgressIndicator();
                      } else if (state is OperacaoLoadedSucessCreateState) {
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
                      return Container();
                    }),
                    const SizedBox(
                      height: 20,
                    ),
                    BlocBuilder<OperacaoBloc, OperacaoState>(
                        builder: (BuildContext context, OperacaoState state) {
                      if (state is OperacaoLoadingState) {
                        return const CircularProgressIndicator();
                      } else if (state is OperacaoLoadedSucessCreateState) {
                        return OperacaoMultiSelectProdutosField(
                            formData: _formData, produtos: state.produtos);
                      }
                      return Container();
                    })
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
    //var produtosFormFieldValid = _formFieldKey.currentState!.validate();
    return generalFieldsValid;
  }

  bool _shouldEnableAddButton(OperacaoState state){
    var result = state is! OperacaoLoadingState && state is! OperacaoLoadedErrorCreateState;
    return result;
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

  void _showLoadErrorDialog(OperacaoLoadedErrorCreateState errorState) async {
    var result = await showDialog<bool>(
        barrierDismissible: false,
        context: context,
        builder: (BuildContext context) {
          return AlertDialog(
            title: const Text('Erro no carregamento'),
            content: Text(errorState.message),
            actions: <Widget>[
              TextButton(
                onPressed: () {
                  Navigator.of(context).pop(true);
                },
                child: const Text('Cancelar'),
              ),
              TextButton(
                onPressed: () {
                  Navigator.of(context).pop(false);
                  BlocProvider.of<OperacaoBloc>(context)
                      .add(GetOperacaoCreateEvent());
                },
                child: const Text('Tente novamente!'),
              ),
            ],
          );
        });

    if (result!) {
      BlocProvider.of<OperacaoBloc>(context).add(GetAllOperacoesEvent());
    }
  }
}
