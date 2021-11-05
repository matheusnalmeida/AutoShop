import 'package:autoshop_application/blocs/operacao/bloc.dart';
import 'package:autoshop_application/blocs/produto/bloc.dart';
import 'package:autoshop_application/blocs/veiculo/bloc.dart';
import 'package:autoshop_application/repositories/repository.dart';
import 'package:autoshop_application/constants/colors.dart';
import 'package:autoshop_application/tools/custom_routes.dart';
import 'package:autoshop_application/tools/material_customcolor_helper.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import 'home/home.dart';

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MultiBlocProvider(
      providers: [
        BlocProvider<VeiculoBloc>(
          create: (BuildContext context) => VeiculoBloc(VeiculoRepository()),
        ),
        BlocProvider<ProdutoBloc>(
          create: (BuildContext context) => ProdutoBloc(ProdutoRepository()),
        ),
        BlocProvider<OperacaoBloc>(
          create: (BuildContext context) => OperacaoBloc(OperacaoRepository()),
        )
      ],
      child: MaterialApp(
        debugShowCheckedModeBanner: false,
        title: 'AutoShop App',
        initialRoute: '/',
        routes: customRoutes,
        theme: ThemeData(
          scaffoldBackgroundColor: AppColor.backgroundColor,
          primarySwatch: MaterialColor(AppColor.headerBarColor.value,
              getSwatch(AppColor.headerBarColor)),
        ),
        home: const HomeScreen(title: 'Home Page'),
      ),
    );
  }
}
