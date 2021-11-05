  import 'package:autoshop_application/ui/widgets/shared/fields_validator.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class OperacaoQuantidadeParcelasField extends StatelessWidget {
  const OperacaoQuantidadeParcelasField({Key? key, required this.formData}) : super(key: key);

  final Map<String, dynamic> formData;

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      enabled: FieldsValidator.isCreate(formData) && !FieldsValidator.isDetails(formData),
      validator: (value) {
        if (value == null || value.isEmpty) {
          return 'Por favor informe a quantidade de parcelas';
        }
        return null;
      },
      initialValue: formData["quantidadeParcelas"].toString(),
      onChanged: (value) => {
        formData["quantidadeParcelas"] = int.parse(value)
      },
      decoration: const InputDecoration(
        labelStyle: TextStyle(fontSize: 25),
        border: OutlineInputBorder(),
        labelText: 'Quantidade de parcelas',
      ),
      keyboardType: TextInputType.number,
      inputFormatters: <TextInputFormatter>[
        FilteringTextInputFormatter.digitsOnly,
        LengthLimitingTextInputFormatter(5),
      ],
    );
  }
}