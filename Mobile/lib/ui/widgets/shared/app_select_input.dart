import 'package:autoshop_application/ui/widgets/shared/fields_validator.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class AppSelectInput<T> extends StatefulWidget {
  final GlobalKey<FormFieldState>? formFieldKey;
  final String hintText;
  final String validatorText;
  final List<String> options;
  final List<T> values;
  final Map<String, dynamic> formData;
  final String formProperty;

  const AppSelectInput(
      {Key? key,
      this.formFieldKey,
      this.hintText = 'Selecione uma opção',
      this.validatorText = 'Por favor selecione um valor',
      this.options = const [],
      this.values = const [],
      required this.formData,
      required this.formProperty})
      : super(key: key);

  @override
  _AppSelectInputState createState() => _AppSelectInputState<T>();
}

class _AppSelectInputState<T> extends State<AppSelectInput<T>> {
  T? selectedValue;

  @override
  Widget build(BuildContext context) {
    var formData = widget.formData;
    selectedValue = formData[widget.formProperty];

    return FormField<T>(
      key: widget.formFieldKey,
      builder: (FormFieldState<T> state) {
        return DropdownButtonHideUnderline(
          child: DropdownButtonFormField<T>(
              validator: (value) {
                if (value == null) {
                  return widget.validatorText;
                }
                return null;
              },                
              decoration: InputDecoration(
                contentPadding: const EdgeInsets.all(15),
                labelStyle: const TextStyle(fontSize: 25),
                border: const OutlineInputBorder(),
                labelText: widget.hintText,
              ),
              style: const TextStyle(fontSize: 20, color: Colors.black),
              value: selectedValue,
              isDense: true,
              onChanged: FieldsValidator.isCreate(formData) &&
                      !FieldsValidator.isDetails(formData)
                  ? (T? newValue) => {
                        FocusScope.of(context).requestFocus(FocusNode()),
                        setState(() {
                          selectedValue = newValue;
                          formData[widget.formProperty] = newValue;
                        })
                      }
                  : null,
              items: widget.values
                  .asMap()
                  .map((index, value) => MapEntry<int, DropdownMenuItem<T>>(
                      index,
                      DropdownMenuItem<T>(
                        value: value,
                        child: Text(widget.options[index].toString()),
                      )))
                  .values
                  .toList()),
        );
      },
    );
  }
}
