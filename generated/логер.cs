using System;

public class Логер {
	public void ЛогироватьDebag(ref string text) {
		throw new System.NotImplementedException("Not implemented");
	}
	public void ЛогироватьInfo(ref string text) {
		throw new System.NotImplementedException("Not implemented");
	}

	private приложение десктоп.Form form;
	private repositories.ПраздникRepository праздникRepository;
	private repositories.ОтделениеRepository отделениеRepository;
	private repositories.ДолжностьRepository должностьRepository;
	private repositories.ПриёмRepository приёмRepository;
	private repositories.РабочийRepository рабочийRepository;
	private Api.Ведение_приёмов ведение_приёмов;
	private Api.Ведение_отделений ведение_отделений;
	private Api.Ведение_праздников ведение_праздников;
	private Api.Ведение_должностей ведение_должностей;
	private Api.Ведение_персонала ведение_персонала;
	private андроид приложение.Аутентификация аутентификация;
	private тгБот.Уведомлятель уведомлятель;

}
