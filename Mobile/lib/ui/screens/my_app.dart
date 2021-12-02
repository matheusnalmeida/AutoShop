import 'package:autoshop_application/blocs/login/login_bloc.dart';
import 'package:autoshop_application/blocs/operacao/bloc.dart';
import 'package:autoshop_application/blocs/produto/bloc.dart';
import 'package:autoshop_application/blocs/usuario/bloc.dart';
import 'package:autoshop_application/blocs/veiculo/bloc.dart';
import 'package:autoshop_application/repositories/repository.dart';
import 'package:autoshop_application/constants/colors.dart';
import 'package:autoshop_application/tools/custom_routes.dart';
import 'package:autoshop_application/tools/material_customcolor_helper.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import 'home/home.dart';
import 'login/login.dart';

class MyApp extends StatelessWidget {
  MyApp({Key? key}) : super(key: key);

  final veiculoRepository = VeiculoRepository();
  final produtoRepository = ProdutoRepository();
  final operacaoRepository = OperacaoRepository();
  final usuarioRepository = UsuarioRepository();
  final loginRepository = LoginRepository();

  @override
  Widget build(BuildContext context) {
    return MultiBlocProvider(
      providers: [
        BlocProvider<VeiculoBloc>(
          create: (BuildContext context) => VeiculoBloc(veiculoRepository),
        ),
        BlocProvider<ProdutoBloc>(
          create: (BuildContext context) => ProdutoBloc(produtoRepository),
        ),
        BlocProvider<OperacaoBloc>(
          create: (BuildContext context) => OperacaoBloc(operacaoRepository, veiculoRepository, produtoRepository),
        ),
        BlocProvider<UsuarioBloc>(
          create: (BuildContext context) => UsuarioBloc(usuarioRepository),
        ),
        BlocProvider<LoginBloc>(
          create: (BuildContext context) => LoginBloc(loginRepository),
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
        home: const LoginForm(),
        //const HomeScreen(title: 'Home Page')
      ),
    );
  }
}
