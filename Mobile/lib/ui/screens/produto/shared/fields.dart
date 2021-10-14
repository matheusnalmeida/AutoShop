import 'package:autoshop_application/ui/widgets/shared/fields_validator.dart';
import 'package:autoshop_application/ui/widgets/shared/regex_input_formatter.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class ProdutoNomeField extends StatelessWidget {
  const ProdutoNomeField({Key? key, required this.formData}) : super(key: key);

  final Map<String, dynamic> formData;

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      enabled: FieldsValidator.isCreate(formData) && !FieldsValidator.isDetails(formData),
      validator: (value) {
        if (value == null || value.isEmpty) {
          return 'Por favor o nome do produto';
        }
        return null;
      },
      initialValue: formData["nome"],
      onChanged: (value) => {
        formData["nome"] = value
      },
      decoration: const InputDecoration(
        labelStyle: TextStyle(fontSize: 25),
        border: OutlineInputBorder(),
        labelText: 'Nome',
      ),
      inputFormatters: <TextInputFormatter>[
        LengthLimitingTextInputFormatter(150),
      ],
    );
  }
}

class ProdutoPrecoField extends StatelessWidget {
  const ProdutoPrecoField({Key? key, required this.formData}) : super(key: key);

  final Map<String, dynamic> formData;
  @override
  Widget build(BuildContext context) {
    return TextFormField(
      enabled: !FieldsValidator.isDetails(formData),
      validator: (value) {
        if (value == null || value.isEmpty) {
          return 'Por favor informe o preço do produto';
        }
        return null;
      },
      initialValue: formData["valor"].toString(),
      onChanged: (value) {
        String _onlyDigits = value.replaceAll(RegExp('[^0-9]'), "");
        double _doubleValue = double.parse(_onlyDigits) / 100;
        formData["valor"] = _doubleValue;
      },      
      decoration: const InputDecoration(
        labelStyle: TextStyle(fontSize: 25),
        border: OutlineInputBorder(),
        labelText: 'Preço',
      ),
      keyboardType: TextInputType.number,
      inputFormatters: [
        FilteringTextInputFormatter.digitsOnly,
        CurrencyPtBrInputFormatter(maxDigits: 18),
      ],
    );
  }
}