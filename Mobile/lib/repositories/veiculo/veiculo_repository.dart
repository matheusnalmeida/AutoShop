import 'dart:async';
import 'dart:convert';
import 'package:autoshop_application/exceptions/http_exception.dart';
import 'package:autoshop_application/models/models.dart';
import 'package:autoshop_application/repositories/veiculo/routing_veiculo.dart';
import 'package:http/http.dart' as http;

class VeiculoRepository extends RoutingVeiculo{

  VeiculoRepository();

  Future<List<Veiculo>> fetchAllVeiculos() async {
    final response = await http.get(Uri.parse(RoutingVeiculo.veiculoURL));

    if (response.statusCode != 200) {
      throw HttpException('Não foi possivel obter a lista de veiculos!');
    }

    final Iterable result = json.decode(response.body);
    return result.map((veiculo) => Veiculo.fromJson(veiculo)).toList();
  }
}