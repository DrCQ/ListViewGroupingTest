Imports System.Globalization

Namespace Converters

    ''' <summary>
    ''' NotNothingToVisibilityConverter class implements the <see cref="IValueConverter"/> interface and allows conversion of Object values into the Visibility value.
    ''' For Not Nothing value this class returns Visibility.Visible otherwise Visibility.Collapsed. If parameters is set the opposite value will be returned.
    ''' </summary>
    <ValueConversion(GetType(Object), GetType(Visibility))>
    Public Class NotNothingToVisibilityConverter
        Implements IValueConverter

        ''' <summary>
        ''' Converts an Object value into Visibility 
        ''' </summary>
        ''' <param name="value">Object value</param>
        ''' <param name="targetType">Visibility</param>
        ''' <param name="parameter">optional</param>
        ''' <param name="culture">not used here</param>
        ''' <returns>Visible or Collapsed</returns>
        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            If targetType IsNot GetType(Visibility) Then Return DependencyProperty.UnsetValue
            Dim bValue As Boolean = True
            If value Is Nothing Then bValue = False
            If parameter IsNot Nothing Then bValue = Not bValue
            Return IIf(bValue, Visibility.Visible, Visibility.Collapsed)
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return DependencyProperty.UnsetValue
        End Function

    End Class

End Namespace

