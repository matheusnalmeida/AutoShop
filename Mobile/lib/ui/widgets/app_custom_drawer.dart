import 'package:autoshop_application/themes/colors.dart';
import 'package:flutter/material.dart';
import 'package:path/path.dart';

import 'custom_list_tile.dart';

class AppCustomDrawer extends StatelessWidget {
  const AppCustomDrawer({Key? key}) : super(key: key);

  
  @override
  Widget build(BuildContext context) {
    return Theme(
      data: Theme.of(context).copyWith(
        canvasColor: AppColor.backgroundColor,
      ),
      child: Drawer(
        child: ListView(
          children: <Widget>[
            const DrawerHeader(
              decoration: BoxDecoration(color: AppColor.drawerHeaderColor),
              child:
                  Image(image: AssetImage('assets/images/autoshop_icon.png')),
            ),
            Column(
              children: [
                CustomListTile(
                    icon: Icons.house,
                    text: 'Home',
                    onTap: () => {doRoute(context, '/')}),
                CustomListTile(
                    icon: Icons.edit, text: 'Editar perfil', onTap: () => {}),
                CustomListTile(
                    icon: Icons.person, text: 'Clientes', onTap: () => {}),
                CustomListTile(
                    icon: Icons.store, text: 'Vendedores', onTap: () => {}),
                CustomListTile(
                    icon: Icons.directions_car,
                    text: 'Veiculos',
                    onTap: () => {doRoute(context, '/veiculos')}),
                CustomListTile(
                    icon: Icons.add_shopping_cart,
                    text: 'Produtos',
                    onTap: () => {}),
                CustomListTile(
                    icon: Icons.monetization_on,
                    text: 'Operação',
                    onTap: () => {})
              ],
            )
          ],
        ),
      ),
    );
  }

  void doRoute(BuildContext context, String name) {
    if (ModalRoute.of(context)?.settings.name != name) {
      Navigator.pushReplacementNamed(context, name);
    }
  }
}
