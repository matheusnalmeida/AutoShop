import 'package:autoshop_application/constants/assets.dart';
import 'package:flutter/material.dart';

class CustomError extends StatelessWidget {
  const CustomError({Key? key, this.customMessage}) : super(key: key);

  final String? customMessage;

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          const Image(image: AssetImage(Assets.ghostError), width: 200),
          const SizedBox(height: 10),
          Text(
            customMessage ?? "Erro ao tentar obter resultados",
            style: const TextStyle(color: Colors.black, fontSize: 20.0),
          )
        ],
      ),
    );
  }
}
