import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class CustomListTile extends StatelessWidget{
  const CustomListTile({Key? key, required this.icon, required this.text, required this.onTap}) : super(key: key);
  
  final IconData icon;
  final String text;
  final Function() onTap;
  
  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.fromLTRB(8.0, 0, 8.0, 0),
      child: InkWell(
        splashColor: Colors.orange,
        onTap: onTap,
        child: SizedBox(
          height: 40,
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: <Widget>[
              Row(
                children: <Widget>[
                  Icon(icon),
                  Padding(
                    padding: const EdgeInsets.all(8.0),
                    child: Text(text,
                        style: const TextStyle(
                        fontSize: 16.0
                      ),
                    ),
                  )
                ]
              ),
              const Icon(Icons.arrow_right)
            ],
          ),
        )
      )
    );
  }
}