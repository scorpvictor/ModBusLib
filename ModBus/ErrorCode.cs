﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Butek.ModBus
{
	/// <summary>
	/// Стандартные коды ошибок
	/// </summary>
	public enum ErrorCode
	{
		/// <summary>
		/// принятый код функции не может быть обработан на  подчиненном.
		/// </summary>
		IllegalFunction = 1,
		/// <summary>
		/// Адрес данных указанный в запросе не доступен данному подчиненному.
		/// </summary>
		IllegaldataAddress = 2,
		/// <summary>
		/// Величина содержащаяся в поле данных запроса является не допустимой величиной для подчиненного.
		/// </summary>
		IllegalDataValue = 3,
		/// <summary>
		/// Невосстанавливаемая ошибка имела место пока подчиненный пытался выполнить затребованное действие.
		/// </summary>
		SlaveDeviceFailure = 4,
		/// <summary>
		/// Подчиненный принял запрос и обрабатывает его,  
		/// но  это   требует много времени.  Этот  ответ предохраняет 
		/// главного от генерации ошибки таймаута.  Главный  может
		/// выдать команду   Poll    Program Complete для  обнаружения 
		/// завершения обработки команды.
		/// </summary>
		Acknowledge = 5,
		/// <summary>
		/// Подчиненный занят обработкой команды.  Главный должен повторить
		/// сообщение позже, когда подчиненный освободится.
		/// </summary>
		SlaveDeviceBusy = 6,
		/// <summary>
		/// Подчиненный не  может  выполнить программную функцию,  принятую в
		/// запросе. Этот  код  возвращается для неудачного программного запроса, 
		/// использующего  функции   с номерами 13 или 14. Главный должен запросить    
		/// диагностическую информацию или   информацию   об ошибках с подчиненного.
		/// </summary>
		NegativeAcknowledge = 7,
		/// <summary>
		/// Подчиненный пытается      читать  расширенную память, но обнаружил ошибку паритета.  
		/// Главный  может повторить запрос,  но  обычно  в таких случаях  требуется  ремонт.
		/// </summary>
		MemoryParityError = 8,
	}
}
