Imports System.Globalization

Namespace Converters

    ''' <summary>
    ''' BooleanToVisibilityConverter class implements the <see cref="IValueConverter"/> interface and allows conversion of Boolean values into the Visibility value. In case the
    ''' <i>parameter</i> value is set, the original value will be changed from True to False or opposite before the final conversion.
    ''' </summary>
    <ValueConversion(GetType(Boolean), GetType(Visibility))>
    Public Class BooleanToVisibilityConverter
        Implements IValueConverter

        ''' <summary>
        ''' Converts an Boolean value into Visibility
        ''' </summary>
        ''' <param name="value">Boolean value</param>
        ''' <param name="targetType">target type - Visibility here</param>
        ''' <param name="parameter">If not Nothing the original value is negated</param>
        ''' <param name="culture">CultureInfo - not used here</param>
        ''' <returns>Visibility result</returns>
        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing OrElse targetType IsNot GetType(Visibility) Then Return DependencyProperty.UnsetValue
            Dim bValue As Boolean = CBool(value)
            If parameter IsNot Nothing Then bValue = Not bValue
            Return IIf(bValue, Visibility.Visible, Visibility.Collapsed)
        End Function

        ''' <summary>
        ''' Not supported here - returns always DependencyProperty.UnsetValue
        ''' </summary>
        ''' <param name="value"></param>
        ''' <param name="targetType"></param>
        ''' <param name="parameter"></param>
        ''' <param name="culture"></param>
        ''' <returns>DependencyProperty.UnsetValue</returns>
        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return DependencyProperty.UnsetValue
        End Function

    End Class

End Namespace

