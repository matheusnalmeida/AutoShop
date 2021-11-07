import 'package:autoshop_application/enums/veiculo_tipo_enum.dart';
import 'package:enum_to_string/enum_to_string.dart';
import 'package:equatable/equatable.dart';

class Veiculo extends Equatable {
  final String? id;
  final String? nome;
  final int? ano;
  final String? modelo;
  final double? preco;
  final String? imagemURL;
  final VeiculoTipoEnum? tipo;

  const Veiculo(
      {this.id,
      this.nome,
      this.ano,
      this.modelo,
      this.preco,
      this.imagemURL,
      this.tipo});

  static Veiculo? fromJson(dynamic json) {
    return json == null ? null : Veiculo(
        id: json['id'],
        nome: json['nome'],
        ano: json['ano'],
        modelo: json['modelo'],
        preco: json['valor'],
        imagemURL: json['imageURL'],
        tipo: EnumToString.fromString(VeiculoTipoEnum.values, json['tipo'])!);
  }

  static Veiculo? jsonMapInsert(dynamic json) {
    return json == null ? null : Veiculo(
        nome: json['nome'],
        ano: json['ano'],
        modelo: json['modelo'],
        preco: json['valor'],
        imagemURL: json['imageURL'],
        tipo: EnumToString.fromString(VeiculoTipoEnum.values, json['tipo'])!);
  }

  static Veiculo? jsonMapUpdate(dynamic json) {
    return json == null ? null : Veiculo(
        id: json['id'],
        preco: json['valor'],
        imagemURL: json['imageURL'],
        tipo: EnumToString.fromString(VeiculoTipoEnum.values, json['tipo'])!);
  }

  @override
  List<Object?> get props => [id];
}
