import 'dart:async';
import 'dart:convert';
import 'package:autoshop_application/exceptions/http_exception.dart';
import 'package:autoshop_application/models/models.dart';
import 'package:autoshop_application/models/results/api_creation_result.dart';
import 'package:autoshop_application/models/results/api_result.dart';
import 'package:autoshop_application/repositories/veiculo/routing_veiculo.dart';
import 'package:autoshop_application/tools/common.dart';
import 'package:http/http.dart' as http;

class VeiculoRepository {
  VeiculoRepository();

  Future<List<Veiculo>> fetchAllVeiculos() async {
    final response = await http.get(RoutingVeiculo.veiculoURL);

    if (response.statusCode != 200) {
      throw HttpException('Não foi possivel obter a lista de veiculos!');
    }

    final Iterable result = json.decode(response.body);
    return result.map((veiculo) => Veiculo.fromJson(veiculo)).toList();
  }

  Future<Veiculo?> fetchByIdAlbum(String id) async {
    final response = await http.get(Uri.parse('${RoutingVeiculo.veiculoURL}/$id'));
    
    var jsonResponse = json.decode(response.body);

    return jsonResponse == null ? null : Veiculo.fromJson(jsonResponse);
  }

  Future<ApiCreationResult> createVeiculo(Veiculo veiculo) async {
    final response = await http.post(
      RoutingVeiculo.veiculoURL,
      headers: Common.apiHeaders,
      body: jsonEncode(<String, dynamic>{
        'nome': veiculo.nome,
        'imageURL': veiculo.imagemURL,
        'ano': veiculo.ano,
        'modelo': veiculo.modelo,
        'valor': veiculo.preco,
        'tipo': veiculo.tipo,
      }),
    );

    return ApiCreationResult.fromJson(json.decode(response.body));
  }

  Future<ApiResult> updateVeiculo(Veiculo veiculo) async {
    final response = await http.put(
      Uri.parse('${RoutingVeiculo.veiculoURL}?id=${veiculo.id}'),
      headers: Common.apiHeaders,
      body: jsonEncode(<String, dynamic>{
        'imageURL': veiculo.imagemURL,
        'valor': veiculo.preco
      }),
    );

    return ApiResult.fromJson(json.decode(response.body));
  }

  Future<ApiResult> deleteAlbum(String id) async {
      final response = await http.delete(
        Uri.parse('${RoutingVeiculo.veiculoURL}/$id'),
        headers: Common.apiHeaders,
      );
      
      return ApiResult.fromJson(json.decode(response.body));
    }
    
}
