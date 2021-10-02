import 'package:autoshop_application/constants/assets.dart';
import 'package:flutter/material.dart';

class NoResultFound extends StatelessWidget {
  const NoResultFound({Key? key, this.customMessage}) : super(key: key);

  final String? customMessage;

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          const Image(image: AssetImage(Assets.noResultFound), width: 225),
          const Text(
            "Nenhum resultado encontrado",
            style: TextStyle(color: Colors.black, fontSize: 23.0),
          ),
          Text(
            customMessage ?? "",
            style: const TextStyle(color: Colors.black, fontSize: 15.0),
          ),
        ],
      ),
    );
  }
}
