import 'dart:convert';
import 'package:http/http.dart' as http;

import '../models/auth_model.dart';
import '../models/reception_model.dart';

class ApiService {
  static const String baseUrl = 'http://10.0.2.2:5270/api/';

  Future<LoginResponse> login(String login, String password) async {
    final url = Uri.parse('${baseUrl}auth/login');
    final response = await http.post(
      url,
      headers: {'Content-Type': 'application/json'},
      body: json.encode(LoginRequest(login: login, password: password).toJson()),
    );

    if (response.statusCode == 200) {
      return LoginResponse.fromJson(json.decode(response.body));
    } else {
      throw Exception('Failed to login');
    }
  }

  Future<List<Reception>> getReceptions(String sid, String login) async {
    final url = Uri.parse('${baseUrl}reception/getByLogin/$login');
    final response = await http.get(
      url,
      headers: {'sid': sid},
    );

    if (response.statusCode == 200) {
      var data = json.decode(response.body) as List;
      return data.map((reception) => Reception.fromJson(reception)).toList();
    } else {
      throw Exception('Failed to load receptions');
    }
  }
}
