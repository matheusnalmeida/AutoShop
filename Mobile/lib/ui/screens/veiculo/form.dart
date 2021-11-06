import 'package:autoshop_application/blocs/veiculo/veiculo_bloc.dart';
import 'package:autoshop_application/constants/colors.dart';
import 'package:autoshop_application/enums/veiculo_tipo_enum.dart';
import 'package:autoshop_application/models/models.dart';
import 'package:autoshop_application/ui/screens/veiculo/shared/fields.dart';
import 'package:autoshop_application/ui/widgets/shared/app_select_input.dart';
import 'package:enum_to_string/enum_to_string.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class VeiculoForm extends StatefulWidget {
  final bool newVeiculo;
  final Veiculo? veiculo;
  final int? index;

  const VeiculoForm(
      {Key? key, required this.newVeiculo, this.veiculo, this.index})
      : super(key: key);

  @override
  _VeiculoFormState createState() => _VeiculoFormState();
}

class _VeiculoFormState extends State<VeiculoForm> {
  final _formKey = GlobalKey<FormState>();
  final Map<String, dynamic> _formData = {};

  @override
  Widget build(BuildContext context) {
    _loadFormData();

    return BlocListener(
      bloc: BlocProvider.of<VeiculoBloc>(context),
      listener: (BuildContext context, VeiculoState state) {
        if (state is VeiculoErrorState) {
          _showErrorDialog(state);
        }
        if (state is VeiculoLoadedSucessState) {
          Navigator.pop(context);
        }
      },
      child: Scaffold(
        appBar: AppBar(
          title: Text(
            widget.newVeiculo ? 'Cadastrar' : 'Editar',
            style: const TextStyle(color: Colors.white),
          ),
          leading: IconButton(
            icon: const Icon(
              Icons.arrow_back,
              color: Colors.white,
            ),
            onPressed: () {
              VeiculoBloc bloc = BlocProvider.of<VeiculoBloc>(context);
              if(bloc.state is VeiculoErrorState){
                BlocProvider.of<VeiculoBloc>(context).add(GetAllVeiculosEvent());
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
                label: Text(widget.newVeiculo ? 'Adicionar' : 'Atualizar'),
                onPressed: () {
                  if (_formKey.currentState!.validate()) {
                    widget.newVeiculo
                        ? BlocProvider.of<VeiculoBloc>(context).add(
                            CreateVeiculoEvent(
                                Veiculo.jsonMapInsert(_formData)))
                        : BlocProvider.of<VeiculoBloc>(context).add(
                            UpdateVeiculoEvent(
                                Veiculo.jsonMapUpdate(_formData)));
                  }
                })),
        body: SingleChildScrollView(
          child: Form(
            key: _formKey,
            child: Padding(
                padding: const EdgeInsets.only(top: 30, left: 10, right: 10),
                child: Column(
                  children: [
                    VeiculoNomeField(formData: _formData),
                    const SizedBox(
                      height: 20,
                    ),
                    VeiculoAnoField(formData: _formData),
                    const SizedBox(
                      height: 20,
                    ),
                    VeiculoModeloField(formData: _formData),
                    const SizedBox(
                      height: 20,
                    ),
                    VeiculoPrecoField(formData: _formData),
                    const SizedBox(
                      height: 20,
                    ),
                    VeiculoImagemUrlField(formData: _formData),
                    const SizedBox(
                      height: 20,
                    ),
                    AppSelectInput<String>(
                      hintText: "Tipo",
                      options: EnumToString.toList(VeiculoTipoEnum.values),
                      values: EnumToString.toList(VeiculoTipoEnum.values),
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
    _formData["isCreate"] = widget.newVeiculo;
    if (!widget.newVeiculo) {
      Veiculo veiculoAtual = widget.veiculo!;
      _formData['id'] = veiculoAtual.id;
      _formData['nome'] = veiculoAtual.nome;
      _formData['ano'] = veiculoAtual.ano;
      _formData['modelo'] = veiculoAtual.modelo;
      _formData['valor'] = "R\$ " + veiculoAtual.preco.toString();
      _formData['imageURL'] = veiculoAtual.imagemURL;
      _formData['tipo'] = EnumToString.convertToString(veiculoAtual.tipo);
    } else {
      _formData['ano'] = "";
      _formData['valor'] = "";
      _formData['tipo'] =
          EnumToString.convertToString(VeiculoTipoEnum.Automovel);
    }
  }

  void _showErrorDialog(VeiculoErrorState errorState) {
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
