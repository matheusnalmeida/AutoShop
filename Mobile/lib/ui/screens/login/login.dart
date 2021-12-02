import 'package:autoshop_application/blocs/login/bloc.dart';
import 'package:autoshop_application/models/login/login.dart';
import 'package:autoshop_application/ui/screens/login/shared/fields.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:jwt_decoder/jwt_decoder.dart';

class LoginForm extends StatefulWidget {
  const LoginForm({Key? key, this.login}) : super(key: key);
  final Login? login;

  @override
  _Login createState() => _Login();
}

class _Login extends State<LoginForm> {
  final _formKey = GlobalKey<FormState>();
  final Map<String, dynamic> _formData = {};

  @override
  Widget build(BuildContext context) {
    return BlocListener(
        bloc: BlocProvider.of<LoginBloc>(context),
        listener: (BuildContext context, LoginState state) {
          if (state is LoginErrorState) {
            _showErrorDialog(state);
          }
          if (state is LoginLoadedSucessState) {
            Navigator.pop(context);
          }
        },
        child: SingleChildScrollView(
          child: Form(
            key: _formKey,
            child: Padding(
                padding: const EdgeInsets.only(top: 30, left: 10, right: 10),
                child: Column(
                  children: [
                    EmailField(formData: _formData),
                    const SizedBox(
                      height: 20,
                    ),
                    SenhaField(formData: _formData),
                    const SizedBox(
                      height: 20,
                    ),
                    Container(
                      width: double.infinity,
                      child: ElevatedButton(
                        onPressed: () async {
                            if (_formKey.currentState!.validate()) {
                              BlocProvider.of<LoginBloc>(context).add(
                                      CreateLoginEvent(
                                          Login.jsonMapInsert(_formData)!));
                            }
                        },
                        child: const Padding(
                          padding: EdgeInsets.all(8.0),
                          child: Text('Logar'),
                        ),
                      ),
                    ),
                  ],
                )),
          ),
        ));
  }

  void _showErrorDialog(LoginErrorState errorState) {
    showDialog(
        context: context,
        builder: (BuildContext context) {
          return AlertDialog(
            title: const Text('Erro no login'),
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
