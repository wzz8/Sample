.586
.model flat,stdcall
option casemap:none

.data
sum dword 0

.code
main proc
	mov eax,5
	add eax,4
	mov sum, eax
	ret
main endp
end