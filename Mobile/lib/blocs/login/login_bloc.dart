import 'package:autoshop_application/exceptions/http_exception.dart';
import 'package:autoshop_application/models/models.dart';
import 'package:autoshop_application/repositories/repository.dart';
import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:shared_preferences/shared_preferences.dart';

part 'login_event.dart';
part 'login_state.dart';

class LoginBloc extends Bloc<LoginEvent, LoginState> {
  final LoginRepository repository;

  LoginBloc(this.repository) : super(const LoginLoadingState()) {
    on<CreateLoginEvent>(_onPostFetched);
  }

  Future<void> _onPostFetched(
      CreateLoginEvent event, Emitter<LoginState> emit) async {
    try {
      emit(const LoginLoadingState());
      var result = (await repository.makeLogin(event.login));
      SharedPreferences prefs = await SharedPreferences.getInstance();
      prefs.setString('token', (result?.token)!);
      return emit(LoginLoadedSucessState(result!));
    } on HttpException catch (ex) {
      return emit(LoginErrorState(ex.message));
    } catch (_) {
      return emit(const LoginErrorState("Erro ao tentar realizar login!"));
    }
  }
}
