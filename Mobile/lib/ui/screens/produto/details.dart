import 'package:autoshop_application/enums/produto_tipo_enum.dart';
import 'package:autoshop_application/models/produto.dart';
import 'package:autoshop_application/ui/screens/produto/shared/fields.dart';
import 'package:autoshop_application/ui/widgets/shared/app_dropdown_input.dart';
import 'package:enum_to_string/enum_to_string.dart';
import 'package:flutter/material.dart';

class ProdutoDetails extends StatefulWidget {
  final bool newProduto;
  final Produto? produto;
  final int? index;

  const ProdutoDetails(
      {Key? key, required this.newProduto, this.produto, this.index})
      : super(key: key);

  @override
  _ProdutoDetailsState createState() => _ProdutoDetailsState();
}

class _ProdutoDetailsState extends State<ProdutoDetails> {
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
        ),
      ),
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
                  AppDropdonwInput<String>(
                    hintText: "Tipo",
                    options: EnumToString.toList(ProdutoTipoEnum.values),
                    formData: _formData,
                    formProperty: "tipo",
                    getLabel: (String value) => value,
                  )
                ],
              )),
        ),
      ),
    );
  }

  void _loadFormData() {
    _formData["isDetails"] = true;
    Produto produtoAtual = widget.produto!;
    _formData['id'] = produtoAtual.id;
    _formData['nome'] = produtoAtual.nome;
    _formData['valor'] = "R\$ " + produtoAtual.preco.toString();
    _formData['tipo'] = EnumToString.convertToString(produtoAtual.tipo);
  }
}
