import 'package:autoshop_application/enums/veiculo_tipo_enum.dart';
import 'package:enum_to_string/enum_to_string.dart';
import 'package:equatable/equatable.dart';

class Veiculo extends Equatable{

  final String id;
  final String nome;
  final int ano;
  final String modelo;
  final double preco;
  final String imagemURL;
  final VeiculoTipoEnum tipo;

  const Veiculo({required this.id, required this.nome, required this.ano, required this.modelo, required this.preco, required this.imagemURL, required this.tipo});
  
  static Veiculo fromJson(dynamic json) {
    return Veiculo(
      id: json['id'],
      nome: json['nome'],
      ano: json['ano'],
      modelo: json['modelo'],
      preco: json['valor'],
      imagemURL: json['imageURL'],
      tipo: EnumToString.fromString(VeiculoTipoEnum.values, json['tipo'])!
    );
  }

  @override
  List<Object?> get props => [id];
}