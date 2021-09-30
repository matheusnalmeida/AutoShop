import 'package:autoshop_application/helpers/material_customcolor_helper.dart';
import 'package:flutter/material.dart';

import 'home/home.dart';

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);
  final headerBarColor = const Color(0xFF956207); 

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'AutoShop App',
      theme: ThemeData(
        scaffoldBackgroundColor: const Color(0xFFFFD3AC),
        primarySwatch: MaterialColor(headerBarColor.value, getSwatch(headerBarColor)),
      ),
      home: const HomeScreen(title: 'Home Page'),
    );
  }
}