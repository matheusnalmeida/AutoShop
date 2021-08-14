import 'package:auto_shop/Shared/Repository/generic_repository.dart';
import 'package:auto_shop/src/Mappers/vehicle_mapper.dart';
import 'package:auto_shop/src/Models/vehicle_model.dart';
import 'package:cloud_firestore/cloud_firestore.dart';

class VehicleRepository{
  final CollectionReference<Vehicle> _vehiclesCollecion;
  
  VehicleRepository() : 
    _vehiclesCollecion = GenericRepository().firestore.collection('vehicles').withConverter<Vehicle>(
      fromFirestore: (snapshot, _) => VehicleMapper().fromJson(snapshot.data()!),
      toFirestore: (vehicle, _) => VehicleMapper().toJson(vehicle),
    );

  Future<List<Vehicle>> getAll() async{
    var vehiclesQueryList = await _vehiclesCollecion.get().then((snapshot) => snapshot.docs);
    var vehiclesList = vehiclesQueryList.map((e){
      var vehicle = e.data();
      vehicle.id = e.id;
      return vehicle;
    }).toList();

    return vehiclesList;
  }

}