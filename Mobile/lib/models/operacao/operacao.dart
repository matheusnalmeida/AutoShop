import 'package:autoshop_application/enums/operacao_situacao_enum.dart';
import 'package:autoshop_application/models/models.dart';
import 'package:enum_to_string/enum_to_string.dart';
import 'package:equatable/equatable.dart';

class Operacao extends Equatable {

  // get & update fields

  final String? id;
  final double? valorTotal;
  final double? valorFinanciado;
  final double? valorVeiculo;
  final int? quantidadeDeParcelas;
  final Veiculo? veiculo;
  final List<Produto>? produtos;
  final OperacaoSituacaoEnum? situacao;
  final DateTime? dataCriacao;

  const Operacao(
    {this.id,
    this.valorTotal,
    this.valorFinanciado,
    this.valorVeiculo,
    this.quantidadeDeParcelas,
    this.veiculo,
    this.produtos,
    this.situacao,
    this.dataCriacao});

  static Operacao fromJson(dynamic json) {
    return Operacao(
        id: json['id'],
        valorTotal: json['valorTotal'],
        valorFinanciado: json['valorFinanciado'],
        valorVeiculo: json['valorVeiculo'],
        quantidadeDeParcelas: json['quantidadeDeParcelas'],
        veiculo: Veiculo.fromJson(json),
        produtos: Produto.fromJsonIterable(json['produtos']),
        situacao: EnumToString.fromString(OperacaoSituacaoEnum.values, json['tipo'])!,
        dataCriacao: json['produtos']);
  }

  static Operacao jsonMapUpdate(dynamic json) {
    return Operacao(
        id: json['id'],
        situacao: json['situacao']);
    }

  @override
  List<Object?> get props => [id];
}