import 'package:autoshop_application/enums/veiculo_tipo_enum.dart';
import 'package:autoshop_application/models/models.dart';
import 'package:autoshop_application/ui/screens/veiculo/shared/fields.dart';
import 'package:autoshop_application/ui/widgets/shared/app_select_input.dart';
import 'package:enum_to_string/enum_to_string.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';

class VeiculoDetails extends StatefulWidget {
  final bool newVeiculo;
  final Veiculo? veiculo;
  final int? index;

  const VeiculoDetails(
      {Key? key, required this.newVeiculo, this.veiculo, this.index})
      : super(key: key);

  @override
  _VeiculoDetailsState createState() => _VeiculoDetailsState();
}

class _VeiculoDetailsState extends State<VeiculoDetails> {
  final _formKey = GlobalKey<FormState>();
  final Map<String, dynamic> _formData = {};

  @override
  Widget build(BuildContext context) {
    _loadFormData();

    return Scaffold(
      appBar: AppBar(
        title: const Text(
          'Detalhes',
          style: TextStyle(color: Colors.white),
        )
      ),
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
    );
  }

  void _loadFormData() {
    _formData["isDetails"] = true;
    Veiculo veiculoAtual = widget.veiculo!;
    _formData['id'] = veiculoAtual.id;
    _formData['nome'] = veiculoAtual.nome;
    _formData['ano'] = veiculoAtual.ano;
    _formData['modelo'] = veiculoAtual.modelo;
    _formData['valor'] = "R\$ " + veiculoAtual.preco.toString();
    _formData['imageURL'] = veiculoAtual.imagemURL;
    _formData['tipo'] = EnumToString.convertToString(veiculoAtual.tipo);
  }
}
