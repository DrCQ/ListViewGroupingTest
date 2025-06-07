Imports System.Globalization

Namespace Converters

    ''' <summary>
    ''' CounterToVisibilityConverter class implements the <see cref="IValueConverter"/> interface and allows conversion of Integer values into the Visibility value.
    ''' For all positive values this class returns Visibility.Visible otherwise Visibility.Collapsed. Setting the parameter inverse the returned values.
    ''' </summary>
    <ValueConversion(GetType(Integer), GetType(Visibility))>
    Public Class CounterToVisibilityConverter
        Implements IValueConverter

        ''' <summary>
        ''' Converts an Integer value into Visibility 
        ''' </summary>
        ''' <param name="value">Integer value</param>
        ''' <param name="targetType">target type - Visibility here</param>
        ''' <param name="parameter">if set, used as threshold value</param>
        ''' <param name="culture">not used here</param>
        ''' <returns>Visibility result</returns>
        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing OrElse targetType IsNot GetType(Visibility) Then Return DependencyProperty.UnsetValue
            Dim cnt As Integer = CInt(value)
            If parameter Is Nothing Then
                Return IIf(cnt > 0, Visibility.Visible, Visibility.Collapsed)
            Else
                Dim pValue As Integer = parameter
                Return IIf(cnt > pValue, Visibility.Visible, Visibility.Collapsed)
            End If
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

