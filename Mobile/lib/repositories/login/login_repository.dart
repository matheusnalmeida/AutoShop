
import 'dart:convert';

import 'package:autoshop_application/models/login/login.dart';
import 'package:autoshop_application/tools/common.dart';
import 'package:http/http.dart' as http;

class LoginRepository {
  LoginRepository();

  Future<Login?> makeLogin(Login login) async {
    final response = await http.post(
    Uri.parse("${Common.apiURL}/Login/GerarToken"),
    body: jsonEncode(<String, dynamic>{
        'email': login.email,
        'senha': login.senha
      }));

    var jsonResponse = json.decode(response.body);

    return jsonResponse == null ? null : Login.fromJson(jsonResponse);
  }
}
