import 'package:autoshop_application/ui/screens/operacao/list.dart';
import 'package:autoshop_application/ui/screens/produto/list.dart';
import 'package:flutter/material.dart';
import 'package:autoshop_application/ui/screens/veiculo/list.dart';

var customRoutes = <String, WidgetBuilder>{
  '/veiculos': (context) => const VeiculoList(),
  '/produtos': (context) => const ProdutoList(),
  '/operacoes': (context) => const OperacaoList(),
};