import 'package:autoshop_application/enums/veiculo_tipo_enum.dart';
import 'package:enum_to_string/enum_to_string.dart';
import 'package:equatable/equatable.dart';

class Login extends Equatable {

  final String? email;
  final String? senha;
  final String? token;

  const Login(
      {this.email,
      this.senha,
      this.token}
      );

  static Login? fromJson(dynamic json) {
    return json == null ? null : Login(
        email: json['email'],
        token: json['token']
        );
  }

  static Login? jsonMapInsert(dynamic json) {
    return json == null ? null : Login(
        email: json['email'],
        senha: json['senha']
        );
  }

  @override
  List<Object?> get props => [token];
}
