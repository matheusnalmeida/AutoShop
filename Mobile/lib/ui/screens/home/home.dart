import 'package:autoshop_application/ui/widgets/custom_list_tile.dart';
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
      drawer: Theme(
        data: Theme.of(context).copyWith(
                 canvasColor: const Color(0xFFFFD3AC),
              ),
        child: Drawer(        
          child: ListView(          
            children: <Widget>[
              const DrawerHeader(
                decoration: BoxDecoration(
                  color: Color(0xFFffe4cc) 
                ),
                child: Image(
                  image: AssetImage('assets/images/autoshop_icon.png')),
                ),
                CustomListTile(icon: Icons.account_balance_rounded , text: 'Editar perfil da concessionária', onTap: () => {}),
                CustomListTile(icon: Icons.edit , text: 'Editar perfil', onTap: () => {}),
                CustomListTile(icon: Icons.person, text: 'Clientes', onTap: () => {}),
                CustomListTile(icon: Icons.store, text: 'Vendedores', onTap: () => {}),
                CustomListTile(icon: Icons.directions_car, text: 'Veiculos', onTap: () => {}),
                CustomListTile(icon: Icons.add_shopping_cart, text: 'Produtos', onTap: () => {}),
                CustomListTile(icon: Icons.monetization_on, text: 'Operação', onTap: () => {})
            ],
          ),
        ),
      ),
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
      ),
      bottomNavigationBar: const BottomAppBar(color: Color(0xFF956207)),
    );
  }
}