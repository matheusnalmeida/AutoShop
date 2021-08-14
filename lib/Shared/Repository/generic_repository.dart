import 'package:cloud_firestore/cloud_firestore.dart';

class GenericRepository{
  static final GenericRepository _genericRepository = GenericRepository._internal();
  FirebaseFirestore firestore = FirebaseFirestore.instance;
  
  GenericRepository._internal() : firestore = FirebaseFirestore.instance;

  factory GenericRepository() {
    return _genericRepository;
  }
}