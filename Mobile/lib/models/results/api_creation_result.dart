import 'package:autoshop_application/models/results/api_result.dart';

class ApiCreationResult extends ApiResult{

  const ApiCreationResult({this.id, required bool sucesso ,required List<String> mensagens}) : super(sucesso: sucesso, mensagens: mensagens);
  
  final String? id;

  static ApiCreationResult fromJson(dynamic json) {
    return ApiCreationResult(
      id: json['id'],
      sucesso: json['sucesso'],
      mensagens: json['mensagens'].cast<String>()
    );
  }
}