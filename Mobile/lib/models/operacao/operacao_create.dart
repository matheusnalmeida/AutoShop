import 'package:equatable/equatable.dart';

class OperacaoCreate extends Equatable {

  final int? quantidadeDeParcelas;
  final String? idVeiculo;
  final String? idCliente;
  final List<String?>? idsProdutos;

  const OperacaoCreate(
    {this.quantidadeDeParcelas,
    this.idVeiculo,
    this.idCliente,
    this.idsProdutos});

  static OperacaoCreate jsonMapInsert(dynamic json) {
    return OperacaoCreate(
        quantidadeDeParcelas: json['quantidadeDeParcelas'],
        idVeiculo: json['idVeiculo'],
        idCliente: json['idCliente'],
        idsProdutos: json['idsProdutos']);
  }

  @override
  List<Object?> get props => [hashCode];
}