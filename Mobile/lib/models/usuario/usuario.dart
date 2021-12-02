import 'package:autoshop_application/enums/usuario_tipo_enum.dart';
import 'package:enum_to_string/enum_to_string.dart';
import 'package:equatable/equatable.dart';

class Usuario extends Equatable {

  final String? id;
  final String? cpf;
  final int? idade;
  final String? telefone;
  final String? email;
  final UsuarioTipoEnum? tipo;
  final String? senha;

  const Usuario(
    {this.id,
    this.cpf,
    this.idade,
    this.telefone,
    this.email,
    this.tipo,
    this.senha});

  static Usuario fromJson(dynamic json) {
    return Usuario(
        id: json['id'],
        cpf: json['cpf'],
        idade: json['idade'],
        telefone: json['telefone'],
        email: json['email'],
        tipo: EnumToString.fromString(UsuarioTipoEnum.values, json['tipo'])!,
        senha: json['senha']);
  }

  static Usuario jsonMapUpdate(dynamic json) {
    return Usuario(
        id: json['id'],
        telefone: json['telefone'],
        email: json['email'],
        senha: json['senha']);
    }

  @override
  List<Object?> get props => [id];
}