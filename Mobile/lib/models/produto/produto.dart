import 'package:autoshop_application/enums/produto_tipo_enum.dart';
import 'package:enum_to_string/enum_to_string.dart';
import 'package:equatable/equatable.dart';

class Produto extends Equatable {
  final String? id;
  final String? nome;
  final double? preco;
  final ProdutoTipoEnum? tipo;

  const Produto(
    {this.id,
    this.nome,
    this.preco,
    this.tipo});

  static Produto? fromJson(dynamic json) {
    return json == null ? null : Produto(
        id: json['id'],
        nome: json['nome'],
        preco: json['preco'],
        tipo: EnumToString.fromString(ProdutoTipoEnum.values, json['tipo'])!);
  }

  static List<Produto?> fromJsonIterable(Iterable json) {
    return json.map((produto) => Produto.fromJson(produto)).toList();
  }

  static Produto jsonMapInsert(dynamic json) {
    return Produto(
        nome: json['nome'],
        preco: json['valor'],
        tipo: EnumToString.fromString(ProdutoTipoEnum.values, json['tipo'])!);
  }

  static Produto jsonMapUpdate(dynamic json) {
    return Produto(
        id: json['id'],
        preco: json['valor'],
        tipo: EnumToString.fromString(ProdutoTipoEnum.values, json['tipo'])!);
  }

  @override
  List<Object?> get props => [id];
}