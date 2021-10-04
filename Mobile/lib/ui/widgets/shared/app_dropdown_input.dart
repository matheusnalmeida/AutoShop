import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class AppDropdonwInput<T> extends StatefulWidget {
  final String hintText;
  final List<T> options;
  final Map<String, dynamic> formData;
  final String formProperty;
  final String Function(T) getLabel;

  const AppDropdonwInput(
      {Key? key,
      this.hintText = 'Selecione uma opção',
      this.options = const [],
      required this.getLabel,
      required this.formData,
      required this.formProperty})
      : super(key: key);

  @override
  _AppDropdonwInputState createState() => _AppDropdonwInputState<T>();
}

class _AppDropdonwInputState<T> extends State<AppDropdonwInput<T>> {
  T? selectedValue;

  @override
  Widget build(BuildContext context) {
    selectedValue = widget.formData[widget.formProperty];

    return FormField<T>(
      enabled: widget.formData["isCreate"],
      builder: (FormFieldState<T> state) {
        return InputDecorator(
          decoration: InputDecoration(
            labelStyle: const TextStyle(fontSize: 25),
            border: const OutlineInputBorder(),
            labelText: widget.hintText,
          ),
          isEmpty: selectedValue == null || selectedValue == '',
          child: DropdownButtonHideUnderline(
            child: DropdownButton<T>(
              value: selectedValue,
              isDense: true,
              onChanged: (T? newValue) => {
                FocusScope.of(context).requestFocus(FocusNode()),
                setState(() {
                  selectedValue = newValue;
                  widget.formData[widget.formProperty] = newValue;
                })
              },
              items: widget.options.map((T value) {
                return DropdownMenuItem<T>(
                  value: value,
                  child: Text(widget.getLabel(value)),
                );
              }).toList(),
            ),
          ),
        );
      },
    );
  }
}
