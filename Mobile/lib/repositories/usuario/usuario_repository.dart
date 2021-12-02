import 'dart:async';
import 'dart:convert';
import 'package:autoshop_application/enums/usuario_tipo_enum.dart';
import 'package:autoshop_application/models/models.dart';
import 'package:autoshop_application/models/results/api_creation_result.dart';
import 'package:autoshop_application/models/results/api_result.dart';
import 'package:autoshop_application/repositories/usuario/routing_usuario.dart';
import 'package:autoshop_application/tools/common.dart';
import 'package:http/http.dart' as http;

class UsuarioRepository {
  UsuarioRepository();

  Future<List<Usuario?>> fetchAllUsuarios() async {
    final response = await http.get(RoutingUsuario.usuarioUrl);

    final Iterable result = json.decode(response.body);
    return result.map((usuario) => Usuario.fromJson(Usuario)).toList();
  }

  Future<Usuario?> fetchByIdUsuario(String id) async {
    final response = await http.get(Uri.parse('${RoutingUsuario.usuarioUrl}/$id'));
    
    var jsonResponse = json.decode(response.body);

    return jsonResponse == null ? null : Usuario.fromJson(jsonResponse);
  }

  Future<ApiCreationResult> createUsuario(Usuario usuario) async {
    final response = await http.post(
      RoutingUsuario.usuarioUrl,
      headers: Common.apiHeaders,
      body: jsonEncode(<String, dynamic>{
        'cpf': usuario.cpf,
        'idade': usuario.idade,
        'telefone': usuario.telefone,
        'email': usuario.email,
        'tipo': usuario.tipo!.value,
        'senha': usuario.senha
      }),
    );

    return ApiCreationResult.fromJson(json.decode(response.body));
  }

  Future<ApiResult> updateUsuario(Usuario usuario) async {
    final response = await http.put(
      Uri.parse('${RoutingUsuario.usuarioUrl}?id=${usuario.id}'),
      headers: Common.apiHeaders,
      body: jsonEncode(<String, dynamic>{
        'telefone': usuario.telefone,
        'email': usuario.email,
        'senha': usuario.senha
      }),
    );

    return ApiResult.fromJson(json.decode(response.body));
  }

  Future<ApiResult> deleteUsuario(String id) async {
      final response = await http.delete(
        Uri.parse('${RoutingUsuario.usuarioUrl}/$id'),
        headers: Common.apiHeaders,
      );
      
      return ApiResult.fromJson(json.decode(response.body));
    }
    
}
