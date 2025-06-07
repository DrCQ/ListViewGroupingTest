Imports System.Globalization

'''<summary>
''' <b>Converters</b> Namespace all classes implementing the <see cref="IValueConverter"/> or <see cref="IMultiValueConverter"/> interfaces and used the XAML View classes.
''' </summary>
Namespace Converters

    ''' <summary>
    ''' BooleanToNotBooleanConverter class implements the <see cref="IValueConverter"/> interface and converts the change the value of the provided Boolean object from True to False and opposite.
    ''' </summary>
    <ValueConversion(GetType(Boolean), GetType(Boolean))>
    Public Class BooleanToNotBooleanConverter
        Implements IValueConverter

        ''' <summary>
        ''' Converts Boolean value to Not Boolean value
        ''' </summary>
        ''' <param name="value">Boolean value</param>
        ''' <param name="targetType">must be Boolean too</param>
        ''' <param name="parameter">not used here</param>
        ''' <param name="culture">not used here</param>
        ''' <returns>Not Boolean value</returns>
        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing OrElse targetType IsNot GetType(Boolean) Then Return DependencyProperty.UnsetValue
            Dim bValue As Boolean = CBool(value)
            Return Not bValue
        End Function

        ''' <summary>
        ''' Not supported here - returns always DependencyProperty.UnsetValue
        ''' </summary>
        ''' <param name="value"></param>
        ''' <param name="targetType"></param>
        ''' <param name="parameter"></param>
        ''' <param name="culture"></param>
        ''' <returns></returns>
        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return DependencyProperty.UnsetValue
        End Function

    End Class

End Namespace


