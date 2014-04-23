using System;
using System.Collections.Generic;
using System.Text;

namespace Butek.ModBus
{
	/// <summary>
	/// Стандартные функции протокола Modbus
	/// </summary>
	public enum Function
	{
		/// <summary>
		/// чтение значений из нескольких регистров флагов
		/// </summary>
		ReadCoilStatus = 1,
		/// <summary>
		/// чтение значений из нескольких дискретных входов
		/// </summary>
		ReadDiscreteInputs = 2,
		/// <summary>
		/// чтение значений из нескольких регистров хранения
		/// </summary>
		ReadHoldingRegisters = 3,
		/// <summary>
		/// чтение значений из нескольких регистров ввода
		/// </summary>
		ReadInputRegisters = 4,
		/// <summary>
		/// запись значения одного флага
		/// </summary>
		ForceSingleCoil = 5,
		/// <summary>
		/// запись значения в один регистр хранения
		/// </summary>
		PresetSingleRegister = 6,
		/// <summary>
		/// запись значений в несколько регистров флагов
		/// </summary>
		ForceMultipleCoils = 15,
		/// <summary>
		/// запись значений в несколько регистров хранения
		/// </summary>
		PresetMultipleRegisters = 16,
	}
}
