import 'package:autoshop_application/constants/colors.dart';
import 'package:autoshop_application/models/produto/produto.dart';
import 'package:autoshop_application/ui/widgets/shared/fields_validator.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:multi_select_flutter/multi_select_flutter.dart';

class OperacaoQuantidadeParcelasField extends StatelessWidget {
  const OperacaoQuantidadeParcelasField({Key? key, required this.formData})
      : super(key: key);

  final Map<String, dynamic> formData;

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      enabled: FieldsValidator.isCreate(formData) &&
          !FieldsValidator.isDetails(formData),
      validator: (value) {
        if (value == null || value.isEmpty) {
          return 'Por favor informe a quantidade de parcelas';
        }
        return null;
      },
      initialValue: formData["quantidadeDeParcelas"].toString(),
      onChanged: (value) => {formData["quantidadeDeParcelas"] = int.parse(value)},
      decoration: const InputDecoration(
        labelStyle: TextStyle(fontSize: 25),
        border: OutlineInputBorder(),
        labelText: 'Quantidade de parcelas',
      ),
      style: const TextStyle(fontSize: 20),
      keyboardType: TextInputType.number,
      inputFormatters: <TextInputFormatter>[
        FilteringTextInputFormatter.digitsOnly,
        LengthLimitingTextInputFormatter(2),
      ],
    );
  }
}

class OperacaoMultiSelectProdutosField extends StatelessWidget {
  const OperacaoMultiSelectProdutosField({Key? key, required this.formData, required this.produtos})
      : super(key: key);

  final Map<String, dynamic> formData;
  final List<Produto?> produtos;
  
  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.only(top: 10.0, bottom: 10.0, left: 5),
      decoration:
          BoxDecoration(border: Border.all(color: const Color(0x61000000))),
      child: MultiSelectDialogField<String?>(
        title: const Text("Selecione"),
        decoration: const BoxDecoration(border: null),
        buttonText: const Text(
          "Produtos",
          style: TextStyle(color: Color(0x99000000), fontSize: 22),
        ),
        backgroundColor: AppColor.backgroundColor,
        buttonIcon: const Icon(Icons.add),
        items: produtos
            .map((produto) => MultiSelectItem(
                produto!.id, "${produto.nome!} - R\$ ${produto.preco!}"))
            .toList(),
        chipDisplay: MultiSelectChipDisplay(
          chipColor: AppColor.headerBarColor,
          textStyle: const TextStyle(color: Colors.white, fontSize: 17),
        ),
        //key: _formFieldKey,
        //validator: (value) {
        //  if (value == null || value.isEmpty) {
        //    return "Selecione ao menos um produto";
        //  }
        //  return null;
        //},
        onConfirm: (values) {
          formData["idsProdutos"] = values;
          //_formFieldKey.currentState!.validate();
        },
      ),
    );
  }
}
