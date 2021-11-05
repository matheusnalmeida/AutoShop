import 'dart:async';
import 'dart:convert';
import 'package:autoshop_application/models/models.dart';
import 'package:autoshop_application/models/results/api_creation_result.dart';
import 'package:autoshop_application/models/results/api_result.dart';
import 'package:autoshop_application/repositories/operacao/routing_operacao.dart';
import 'package:autoshop_application/tools/common.dart';
import 'package:http/http.dart' as http;

class OperacaoRepository {
  OperacaoRepository();

  Future<List<Operacao>> fetchAllOperacoes() async {
    final response = await http.get(RoutingOperacao.operacaoURL);

    final Iterable result = json.decode(response.body);
    return result.map((operacao) => Operacao.fromJson(operacao)).toList();
  }

  Future<Operacao?> fetchByIdOperacao(String id) async {
    final response = await http.get(Uri.parse('${RoutingOperacao.operacaoURL}/$id'));
    
    var jsonResponse = json.decode(response.body);

    return jsonResponse == null ? null : Operacao.fromJson(jsonResponse);
  }

  Future<ApiCreationResult> createOperacao(OperacaoCreate operacao) async {
    final response = await http.post(
      RoutingOperacao.operacaoURL,
      headers: Common.apiHeaders,
      body: jsonEncode(<String, dynamic>{
        'quantidadeDeParcelas': operacao.quantidadeDeParcelas,
        'idVeiculo': operacao.idVeiculo,
        'idCliente': operacao.idCliente,
        'idsProdutos': operacao.idsProdutos
      }),
    );

    return ApiCreationResult.fromJson(json.decode(response.body));
  }

  Future<ApiResult> updateOperacao(Operacao operacao) async {
    final response = await http.put(
      Uri.parse('${RoutingOperacao.operacaoURL}?id=${operacao.id}'),
      headers: Common.apiHeaders,
      body: jsonEncode(<String, dynamic>{
        'situacao': operacao.situacao
      }),
    );

    return ApiResult.fromJson(json.decode(response.body));
  }

  //Future<ApiResult> deleteOperacao(String id) async {
  //    final response = await http.delete(
  //      Uri.parse('${RoutingOperacao.operacaoURL}/$id'),
  //      headers: Common.apiHeaders,
  //    );
  //    
  //    return ApiResult.fromJson(json.decode(response.body));
  //}
    
}
