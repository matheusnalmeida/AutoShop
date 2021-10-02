import 'package:autoshop_application/ui/widgets/app_custom_drawer.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';

class HomeScreen extends StatelessWidget {
  const HomeScreen({Key? key, required this.title}) : super(key: key);
  
  final String title;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(title),
      ),
      drawer: const AppCustomDrawer(),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.start,
          children: const <Widget>[
            Image(
              image: AssetImage('assets/images/autoshop_icon.png'),
              width: 175),
            Text(
              'Bem vindo ao auto shop!',
              style: TextStyle(
                fontWeight: FontWeight.bold,
                fontSize: 23,
                fontFamily: 'consolas'),
            ),
          ],
        ),
      )
    );
  }
}