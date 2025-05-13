class LoginRequest {
  String login;
  String password;

  LoginRequest({required this.login, required this.password});

  Map<String, dynamic> toJson() {
    return {
      'Login': login,
      'Password': password,
    };
  }
}

class LoginResponse {
  String sid;

  LoginResponse({required this.sid});

  factory LoginResponse.fromJson(Map<String, dynamic> json) {
    return LoginResponse(
      sid: json['sid'],
    );
  }
}
