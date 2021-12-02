
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class EmailField extends StatelessWidget {
  const EmailField({Key? key, required this.formData}) : super(key: key);

  final Map<String, dynamic> formData;

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      validator: (value) {
        if (value == null || value.isEmpty) {
          return 'Informe o email';
        }
        return null;
      },
      initialValue: formData["email"],
      onChanged: (value) => {
        formData["email"] = value
      },
      decoration: const InputDecoration(
        labelStyle: TextStyle(fontSize: 25),
        border: OutlineInputBorder(),
        labelText: 'Email',
      ),
      style: const TextStyle(fontSize: 20),
      inputFormatters: <TextInputFormatter>[
        LengthLimitingTextInputFormatter(150),
      ],
    );
  }
}

class SenhaField extends StatelessWidget {
  const SenhaField({Key? key, required this.formData}) : super(key: key);

  final Map<String, dynamic> formData;

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      validator: (value) {
        if (value == null || value.isEmpty) {
          return 'Informe a senha';
        }
        return null;
      },
      initialValue: formData["senha"],
      onChanged: (value) => {
        formData["senha"] = value
      },
      decoration: const InputDecoration(
        labelStyle: TextStyle(fontSize: 25),
        border: OutlineInputBorder(),
        labelText: 'Senha',
      ),
      style: const TextStyle(fontSize: 20),
      inputFormatters: <TextInputFormatter>[
        LengthLimitingTextInputFormatter(150),
      ],
    );
  }
}