import 'package:equatable/equatable.dart';

class ApiResult extends Equatable{
  const ApiResult({required this.sucesso, required this.mensagens});

  final bool sucesso;
  final List<String> mensagens;
  
  static ApiResult fromJson(dynamic json) {
    return ApiResult(
      sucesso: json['sucesso'],
      mensagens: json['mensagens'].cast<String>()
    );
  }

  @override
  List<Object?> get props => [];
}