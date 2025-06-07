Imports System.Globalization

Namespace Converters

    ''' <summary>
    ''' NotNothingToBooleanConverter class implements the <see cref="IValueConverter"/> interface and allows conversion of Object values into the Boolean value.
    ''' For Not Nothing value this class returns True otherwise False.
    ''' </summary>
    <ValueConversion(GetType(Object), GetType(Boolean))>
    Public Class NotNothingToBooleanConverter
        Implements IValueConverter

        ''' <summary>
        ''' Converts an Object value into Boolean 
        ''' </summary>
        ''' <param name="value">Object value</param>
        ''' <param name="targetType">Boolean</param>
        ''' <param name="parameter">optional</param>
        ''' <param name="culture">not used here</param>
        ''' <returns>True or False</returns>
        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return (value IsNot Nothing)
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return DependencyProperty.UnsetValue
        End Function
    End Class

End Namespace


