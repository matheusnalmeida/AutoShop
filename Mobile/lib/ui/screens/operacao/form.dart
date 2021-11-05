import 'package:autoshop_application/blocs/operacao/operacao_bloc.dart';
import 'package:autoshop_application/constants/colors.dart';
import 'package:autoshop_application/models/models.dart';
import 'package:autoshop_application/ui/screens/operacao/shared/fields.dart';
import 'package:autoshop_application/ui/widgets/shared/app_dropdown_input.dart';
import 'package:enum_to_string/enum_to_string.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

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
  final Map<String, dynamic> _formData = {};

  @override
  Widget build(BuildContext context) {
    _loadFormData();

    return BlocListener(
      bloc: BlocProvider.of<OperacaoBloc>(context),
      listener: (BuildContext context, OperacaoState state) {
        if (state is ErrorState) {
          _showErrorDialog(state);
        }
        if (state is LoadedSucessState) {
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
              if(bloc.state is ErrorState){
                BlocProvider.of<OperacaoBloc>(context).add(GetAllOperacoesEvent());
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
                  if (_formKey.currentState!.validate()) {
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
                    )
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
    }
  }

  void _showErrorDialog(ErrorState errorState) {
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
