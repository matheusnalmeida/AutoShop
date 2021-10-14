import 'dart:async';
import 'dart:convert';
import 'package:autoshop_application/models/models.dart';
import 'package:autoshop_application/models/results/api_creation_result.dart';
import 'package:autoshop_application/models/results/api_result.dart';
import 'package:autoshop_application/repositories/produto/routing_produto.dart';
import 'package:autoshop_application/tools/common.dart';
import 'package:http/http.dart' as http;

class ProdutoRepository {
  ProdutoRepository();

  Future<List<Produto>> fetchAllProdutos() async {
    final response = await http.get(RoutingProduto.produtoURL);

    final Iterable result = json.decode(response.body);
    return result.map((produto) => Produto.fromJson(produto)).toList();
  }

  Future<Produto?> fetchByIdAlbum(String id) async {
    final response = await http.get(Uri.parse('${RoutingProduto.produtoURL}/$id'));
    
    var jsonResponse = json.decode(response.body);

    return jsonResponse == null ? null : Produto.fromJson(jsonResponse);
  }

  Future<ApiCreationResult> createProduto(Produto produto) async {
    final response = await http.post(
      RoutingProduto.produtoURL,
      headers: Common.apiHeaders,
      body: jsonEncode(<String, dynamic>{
        'nome': produto.nome,
        'preco': produto.preco,
        'tipo': produto.tipo!.index + 1,
      }),
    );

    return ApiCreationResult.fromJson(json.decode(response.body));
  }

  Future<ApiResult> updateProduto(Produto produto) async {
    final response = await http.put(
      Uri.parse('${RoutingProduto.produtoURL}?id=${produto.id}'),
      headers: Common.apiHeaders,
      body: jsonEncode(<String, dynamic>{
        'preco': produto.preco
      }),
    );

    return ApiResult.fromJson(json.decode(response.body));
  }

  Future<ApiResult> deleteProduto(String id) async {
      final response = await http.delete(
        Uri.parse('${RoutingProduto.produtoURL}/$id'),
        headers: Common.apiHeaders,
      );
      
      return ApiResult.fromJson(json.decode(response.body));
    }
    
}
