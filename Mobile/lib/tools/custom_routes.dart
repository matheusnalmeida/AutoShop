import 'package:autoshop_application/ui/screens/home/home.dart';
import 'package:flutter/material.dart';
import 'package:autoshop_application/ui/screens/veiculo/list.dart';

var customRoutes = <String, WidgetBuilder>{
  '/veiculos': (context) => const VeiculoList(),
};