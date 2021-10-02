import 'package:autoshop_application/blocs/veiculo/bloc.dart';
import 'package:autoshop_application/models/models.dart';
import 'package:autoshop_application/constants/colors.dart';
import 'package:autoshop_application/ui/widgets/app_custom_drawer.dart';
import 'package:autoshop_application/ui/widgets/shared/error.dart';
import 'package:autoshop_application/ui/widgets/shared/noresult_found.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class VeiculoList extends StatefulWidget {
  const VeiculoList({Key? key}) : super(key: key);

  @override
  _VeiculoListState createState() => _VeiculoListState();
}

class _VeiculoListState extends State<VeiculoList> {
  @override
  void initState() {
    super.initState();
    BlocProvider.of<VeiculoBloc>(context).add(GetAllVeiculosEvent());
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(),
      floatingActionButton: FloatingActionButton(
        onPressed: () {},
        child: const Icon(Icons.add),
        backgroundColor: AppColor.headerBarColor,
      ),
      drawer: const AppCustomDrawer(),
      body: Center(child: BlocBuilder<VeiculoBloc, VeiculoState>(
          builder: (BuildContext context, VeiculoState state) {
        if (state is LoadingState) {
          return const Center(
            child: CircularProgressIndicator(),
          );
        }
        if (state is ErrorState) {
          return CustomError(customMessage: state.message);
        }
        if (state is LoadedSucessState) {
          if (state.veiculos.isEmpty) {
            return const NoResultFound(
                customMessage: "Nenhum veiculo cadastrado");
          }
          return ListView.builder(
            itemCount: state.veiculos.length,
            itemBuilder: (context, index) {
              final item = state.veiculos[index];
              return _vehicleCard(item, context);
            },
          );
        }
        return Container();
      })),
    );
  }

  Card _vehicleCard(Veiculo veiculo, BuildContext context) {
    return Card(
      color: AppColor.vehicleCardColor,
      elevation: 8.0,
      margin: const EdgeInsets.symmetric(horizontal: 10.0, vertical: 6.0),
      child: Row(
        children: [
          Flexible(
            child: Container(
              child: _vehicleListTitle(veiculo, context),
            ),
          ),
          IconButton(
            onPressed: () => {},
            icon: const Icon(Icons.edit, color: Colors.yellow),
          ),
          IconButton(
            onPressed: () => {},
            icon: const Icon(Icons.delete, color: Colors.red)
          ),
        ],
      ),
    );
  }

  ListTile _vehicleListTitle(Veiculo veiculo, BuildContext context) {
    return ListTile(
        contentPadding:
            const EdgeInsets.symmetric(horizontal: 20.0, vertical: 5.0),
        leading: Container(
          padding: const EdgeInsets.only(right: 12.0),
          decoration: const BoxDecoration(
              border:
                  Border(right: BorderSide(width: 1.0, color: Colors.white24))),
          child: Image.network(veiculo.imagemURL),
        ),
        title: Text(
          veiculo.nome,
          style: const TextStyle(
              color: Colors.white, fontWeight: FontWeight.bold, fontSize: 20),
        ),
        subtitle: Row(
          children: <Widget>[
            Text(veiculo.modelo,
                style: const TextStyle(color: Colors.white, fontSize: 20))
          ],
        ));
  }

  @override
  void dispose() {
    super.dispose();
  }
}
