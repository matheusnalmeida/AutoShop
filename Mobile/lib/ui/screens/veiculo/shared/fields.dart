import 'package:autoshop_application/ui/widgets/shared/fields_validator.dart';
import 'package:autoshop_application/ui/widgets/shared/regex_input_formatter.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class VeiculoNomeField extends StatelessWidget {
  const VeiculoNomeField({Key? key, required this.formData}) : super(key: key);

  final Map<String, dynamic> formData;

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      enabled: FieldsValidator.isCreate(formData) || !FieldsValidator.isDetails(formData),
      validator: (value) {
        if (value == null || value.isEmpty) {
          return 'Por favor o nome do veiculo';
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

class VeiculoAnoField extends StatelessWidget {
  const VeiculoAnoField({Key? key, required this.formData}) : super(key: key);

  final Map<String, dynamic> formData;

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      enabled: FieldsValidator.isCreate(formData) || !FieldsValidator.isDetails(formData),
      validator: (value) {
        if (value == null || value.isEmpty) {
          return 'Por favor o ano do veiculo';
        }
        return null;
      },
      initialValue: formData["ano"].toString(),
      onChanged: (value) => {
        formData["ano"] = int.parse(value)
      },
      decoration: const InputDecoration(
        labelStyle: TextStyle(fontSize: 25),
        border: OutlineInputBorder(),
        labelText: 'Ano',
      ),
      keyboardType: TextInputType.number,
      inputFormatters: <TextInputFormatter>[
        FilteringTextInputFormatter.digitsOnly,
        LengthLimitingTextInputFormatter(5),
      ],
    );
  }
}

class VeiculoModeloField extends StatelessWidget {
  const VeiculoModeloField({Key? key, required this.formData})
      : super(key: key);

  final Map<String, dynamic> formData;

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      enabled: FieldsValidator.isCreate(formData) || !FieldsValidator.isDetails(formData),
      validator: (value) {
        if (value == null || value.isEmpty) {
          return 'Por favor informe o modelo do veiculo';
        }
        return null;
      },
      initialValue: formData["modelo"],
      onChanged: (value) => {
        formData["modelo"] = value
      },
      decoration: const InputDecoration(
        labelStyle: TextStyle(fontSize: 25),
        border: OutlineInputBorder(),
        labelText: 'Modelo',
      ),
      keyboardType: TextInputType.number,
      inputFormatters: <TextInputFormatter>[
        LengthLimitingTextInputFormatter(150),
      ],
    );
  }
}

class VeiculoPrecoField extends StatelessWidget {
  const VeiculoPrecoField({Key? key, required this.formData}) : super(key: key);

  final Map<String, dynamic> formData;
  /*
      String _onlyDigits = value.replaceAll(RegExp('[^0-9]'), "");
    double _doubleValue = double.parse(_onlyDigits) / 100;
    return _valor = _doubleValue;
  */
  @override
  Widget build(BuildContext context) {
    return TextFormField(
      enabled: !FieldsValidator.isDetails(formData),
      validator: (value) {
        if (value == null || value.isEmpty) {
          return 'Por favor informe o preço do veiculo';
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

class VeiculoImagemUrlField extends StatelessWidget {
  const VeiculoImagemUrlField({Key? key, required this.formData})
      : super(key: key);

  final Map<String, dynamic> formData;

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      enabled: !FieldsValidator.isDetails(formData),
      validator: (value) {
        if (value == null || value.isEmpty) {
          return 'Por favor informe a url para a imagem do veiculo';
        }
        return null;
      },
      initialValue: formData["imageURL"],
      onChanged: (value) => {
        formData["imageURL"] = value
      },      
      decoration: const InputDecoration(
        labelStyle: TextStyle(fontSize: 25),
        border: OutlineInputBorder(),
        labelText: 'Imagem URL',
      ),
      inputFormatters: <TextInputFormatter>[
        LengthLimitingTextInputFormatter(150),
      ],
    );
  }
}