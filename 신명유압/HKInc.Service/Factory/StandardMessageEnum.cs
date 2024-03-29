﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Service.Factory
{
    public enum StandardMessageEnum : int
    {
        /// <summary>
        /// 저장하지 않은 변경사항이 있습니다. 먼저 저장 하시겠습니까?
        /// </summary>
        M_1 = 1,
        /// <summary>
        /// [{0}]건의 데이터가 조회되었습니다. 
        /// </summary>
        M_2 = 2,
        /// <summary>
        /// 저장되었습니다.
        /// </summary>
        M_3 = 3, 
        /// <summary>
        /// 수행중인 모든 화면을 닫고 재시작 하시겠습니까?
        /// </summary>
        M_4 = 4, 
        /// <summary>
        /// 저장할 데이터가 없습니다.
        /// </summary>
        M_5 = 5, 
        /// <summary>
        /// 사용자ID와 패스워드를 확인해 주세요.
        /// </summary>
        M_6 = 6, 
        /// <summary>
        /// 사용자ID를 확인해 주세요.
        /// </summary>
        M_7 = 7, 
        /// <summary>
        /// 패스워드를 확인해 주세요.
        /// </summary>
        M_8 = 8, 
        /// <summary>
        /// 변경 할 비밀번호가 형식에 맞지 않습니다.
        /// </summary>
        M_9 = 9, 
        /// <summary>
        /// 해당 마스터에 디테일이 존재합니다. 삭제(변경)를 하기위해서는 먼저 디테일 코드를 삭제 하십시오.
        /// </summary>
        M_10 = 10, 
        /// <summary>
        /// 비밀번호는 최소 하나의 소문자를 포함해야 합니다.
        /// </summary>
        M_11 = 11, 
        /// <summary>
        /// 비밀번호는 최소 하나의 대문자를 포함해야 합니다.
        /// </summary>
        M_12 = 12, 
        /// <summary>
        /// 비밀번호는 최소 8 자리 최대 15자리 이어야 합니다.
        /// </summary>
        M_13 = 13, 
        /// <summary>
        /// 비밀번호는 최소 하나의 숫자를 포함해야 합니다.
        /// </summary>
        M_14 = 14, 
        /// <summary>
        /// 비밀번호는 최소 하나의 특수문자를 포함해야 합니다.
        /// </summary>
        M_15 = 15, 
        /// <summary>
        /// 비밀번호가 확인비밀번호와 일치하지 않습니다.
        /// </summary>
        M_16 = 16, 
        /// <summary>
        /// - 컬럼: "{0}", 에러: "{1}"{2}
        /// </summary>
        M_20 = 20, 
        /// <summary>
        /// 테이블 :  "{0}" 상태 "{1}" 가 다음의 에러를 포함하고 있습니다:{2}
        /// </summary>
        M_21 = 21, 
        /// <summary>
        /// 프리뷰모드로 파일을 열겠습니까?
        /// </summary>
        M_22 = 22, 
        /// <summary>
        /// 정말로 종료하시겠습니까?
        /// </summary>
        M_30 = 30, 
        /// <summary>
        /// {0}가(이) 존재하지 않습니다.
        /// </summary>
        M_32 = 32, 
        /// <summary>
        /// {0}가(이) 완료되지 않았습니다.
        /// </summary>
        M_33 = 33, 
        /// <summary>
        /// {0}가(이) 없습니다.
        /// </summary>
        M_34 = 34, 
        /// <summary>
        /// {0}가(이) 부족합니다.
        /// </summary>
        M_35 = 35, 
        /// <summary>
        /// {0}을(를) 정말 삭제 하시겠습니까?
        /// </summary>
        M_37 = 37,
        /// <summary>
        /// {0}은(는) 수정할 수 없습니다.
        /// </summary>
        M_38 = 38, 
        /// <summary>
        /// 필수값을 입력해 주시기 바랍니다
        /// </summary>
        M_39 = 39, 
        /// <summary>
        /// {0}은(는) 필수입니다.
        /// </summary>
        M_40 = 40,
        /// <summary>
        /// 이미 등록된 {0}가(이) 있습니다.
        /// </summary>
        M_41 = 41,
        /// <summary>
        /// 등록된 UI가 존재합니다. 덮어 쓰시겠습니까?
        /// </summary>
        M_42 = 42,
        /// <summary>
        /// UI가 저장 되었습니다.
        /// </summary>
        M_43 = 43,
        /// <summary>
        /// UI가 초기화 되었습니다. 화면을 닫은 후 재실행 바랍니다.
        /// </summary>
        M_44 = 44,
        /// <summary>
        /// FTP 전송중 문제가 발생하였습니다.네트워크 상황 또는 접속정보를 살펴 보시기 바랍니다.
        /// </summary>
        M_45 = 45,
        /// <summary>
        /// 이 파일을 여시겠습니까?
        /// </summary>
        M_46 = 46,
        /// <summary>
        /// {0}의 삭제는 시스템 전체에 영향을 미칩니다. 사용여부 해제를 추천 합니다. 예(Y) : 삭제, 아니오(N) : 사용여부해제, 취소(C) : 취소
        /// </summary>
        M_47 = 47,
        /// <summary>
        /// {0}에 {1}이(가) 존재합니다.삭제(변경)를 하기위해서는 먼저 {2}을(를) 삭제 하십시오.
        /// </summary>
        M_48 = 48,
        /// <summary>
        /// {0} 또는 {1}은(는) 필수입니다.
        /// </summary>
        M_49 = 49,
        /// <summary>
        /// {0}의 삭제는 시스템 전체에 영향을 미치므로 삭제가 불가합니다. 사용여부 해제를 하시겠습니까?
        /// </summary>
        M_50 = 50,
        /// <summary>
        /// {0}가(이) 존재하므로 삭제를 할 수 없습니다.
        /// </summary>
        M_51 = 51,
        /// <summary>
        /// {0}가(이) 존재하므로 수정을 할 수 없습니다.
        /// </summary>
        M_52 = 52,
        /// <summary>
        /// 작업지시번호 : {0}는 생산이 이미 진행된 공정이 있어 삭제가 불가합니다.
        /// </summary>
        M_53 = 53,
        /// <summary>
        /// 설비가 필수인 공정이 존재합니다. 빨간 배경의 설비명은 필수이므로 입력해 주시기 바랍니다.
        /// </summary>
        M_54 = 54,
        /// <summary>
        /// {0}을(를) 풀고 시도해 주시기 바랍니다.
        /// </summary>
        M_55 = 55,
        /// <summary>
        /// {0}은(는) 삭제할 수 없습니다.
        /// </summary>
        M_56 = 56,
        /// <summary>
        /// {0} 이상은 불가합니다.
        /// </summary>
        M_57 = 57,
        /// <summary>
        /// {0}이(가) 없는 항목이 존재합니다. 입력 후 시도해 주시기 바랍니다.
        /// </summary>
        M_58 = 58,
        /// <summary>
        /// {0}이(가) 불합격(NG) 입니다.
        /// </summary>
        M_59 = 59,
        /// <summary>
        /// 입력한 입고 LOT NO의 이전 LOT NO 재고가 남아 있습니다. 선입선출 확인 필요. 무시하고 진행하겠습니까?
        /// </summary>
        M_60 = 60,
        /// <summary>
        /// 재입고 가능량보다 재입고량이 더 많은 것이 존재합니다. 확인해 주시기 바랍니다.
        /// </summary>
        M_61 = 61,
        /// <summary>
        /// 재입고가 완료되었습니다. 자재재고관리에서 확인해 주시기 바랍니다.
        /// </summary>
        M_62 = 62,
        /// <summary>
        /// 마지막 항목입니다.
        /// </summary>
        M_63 = 63,
        /// <summary>
        /// 해당 작업지시서의 이동표 번호가 아닙니다.
        /// </summary>
        M_64 = 64,
        /// <summary>
        /// 해당 제품정보에 원자재가 등록되어 있지 않습니다. 
        /// </summary>
        M_65 = 65,
        /// <summary>
        /// 해당 제품정보의 원자재와 맞지 않은 원자재입니다.
        /// </summary>
        M_66 = 66,
        /// <summary>
        /// 품목정보에 {0}이(가) 미사용입니다.
        /// </summary>
        M_67 = 67,
        /// <summary>
        /// {0}이(가) 존재합니다. {1}을(를) 기입해 주시기 바랍니다.
        /// </summary>
        M_68 = 68,
        /// <summary>
        /// {0}이(가) {1}보다 클 수 없습니다.
        /// </summary>
        M_69 = 69,
        /// <summary>
        /// {0}이(가) {1}보다 작을 수 없습니다.
        /// </summary>
        M_70 = 70,
        /// <summary>
        /// 툴 수명을 초과하였습니다. 툴 교체를 해주시기 바랍니다.
        /// </summary>
        M_71 = 71,
        /// <summary>
        /// 원자재 교체 시 생산 LOT NO가 변경됩니다.
        /// 기존 생산 LOT NO의 실적을 그만두고 정말 교체 하시겠습니까?
        /// </summary>
        M_72 = 72,
        /// <summary>
        /// 이미 사용중인 이동표 번호입니다.
        /// </summary>
        M_73 = 73,
        /// <summary>
        /// 이동표 교체 시 생산 LOT NO가 변경됩니다.
        /// 기존 생산 LOT NO의 실적을 그만두고 정말 교체 하시겠습니까?
        /// </summary>
        M_74 = 74,
        /// <summary>
        /// 정말로 아래 모든 항목에 출고예정일을 일괄적용 하시겠습니까?
        /// </summary>
        M_75 = 75,
        /// <summary>
        /// 현재 마감확정이 되지 않은 항목이 존재하여 {0}할 수 없습니다.
        /// </summary>
        M_76 = 76,
        /// <summary>
        /// 현 마감월 이후 등록된 마감 데이터가 존재하여 취소할 수 없습니다.
        /// </summary>
        M_77 = 77,
        /// <summary>
        /// 해당 {0}에 표준공정을 등록해 주시기 바랍니다.
        /// </summary>
        M_78 = 78,
        /// <summary>
        /// 해당 제품정보의 반제품와 맞지 않은 반제품입니다.
        /// </summary>
        M_79 = 79,
        /// <summary>
        /// 반제품 교체 시 생산 LOT NO가 변경됩니다.
        /// 기존 생산 LOT NO의 실적을 그만두고 정말 교체 하시겠습니까?
        /// </summary>
        M_80 = 80,
        /// <summary>
        /// 재입고가 완료되었습니다. 반제품재고관리에서 확인해 주시기 바랍니다.
        /// </summary>
        M_81 = 81,
        /// <summary>
        /// 당월 마감 처리는 익월 가능합니다.
        /// </summary>
        M_82 = 82,
        /// <summary>
        /// 작업지시 저장후 출력하십시오.
        /// </summary>
        M_83 = 83,
        /// <summary>
        /// {0}을 선택하십시오.
        /// </summary>
        M_84 = 84,
        /// <summary>
        /// 이미지 파일(BMP,JPG,GIF,PNG)만 가능합니다.
        /// </summary>
        M_85 = 85,
        /// <summary>
        /// 현 재고량은 {0}이므로 그 이상 출고할 수 없습니다.
        /// </summary>
        M_86 = 86,
        /// <summary>
        /// {0}은(는) 중복될 수 없습니다.
        /// </summary>
        M_87 = 87,
        /// <summary>
        /// 현 창고위치와 동일합니다. 확인 부탁드립니다.
        /// </summary>
        M_88 = 88,
        /// <summary>
        /// 마감 처리 취소가 아니므로 삭제할 수 없습니다..
        /// </summary>
        M_89 = 89,
        /// <summary>
        /// 마지막 마감 항목부터 처리할 수 있습니다..
        /// </summary>
        M_90 = 90,
        /// <summary>
        /// 지시수량보다 총 생산량이 부족합니다. 무시하고 종료하시겠습니까?
        /// </summary>
        M_91 = 91,
        /// <summary>
        /// 이전 공정이 끝나지 않았습니다.
        /// </summary>
        M_92 = 92,
        /// <summary>
        /// 최상위에 대해서만 하위추가를 하실 수 있습니다.
        /// </summary>
        M_93 = 93,
        /// <summary>
        /// 거래명세서는 같은 거래처로 출력이 가능합니다. 같은 거래처를 선택해 주세요.
        /// </summary>
        M_94 = 94,
        /// <summary>
        /// 거래명세서에 단가를 입력하시겠습니까?
        /// </summary>
        M_95 = 95,
        /// <summary>
        /// 측정치1에 값이 없으므로 적용할 수 없습니다.
        /// </summary>
        M_96 = 96,
        /// <summary>
        /// 측정치2에 값이 없으므로 적용할 수 없습니다.
        /// </summary>
        M_97 = 97,
        /// <summary>
        /// 측정치2가 1보다 값이 크므로 적용할 수 없습니다.
        /// </summary>
        M_98 = 98,
        /// <summary>
        /// 저장후 시도해 주시기 바랍니다.
        /// </summary>
        M_99 = 99,
        /// <summary>
        /// 작업지시가 이미 존재합니다. 삭제 후 시도해 주시기 바랍니다.
        /// </summary>
        M_100 = 100,
        /// <summary>
        /// 작업지시서가 이미 출력되었습니다. 그래도 삭제하시겠습니까?
        /// </summary>
        M_101 = 101,
        /// <summary>
        /// 일일출고계획 데이터가 존재하여 삭제할 수 없습니다.
        /// </summary>
        M_102 = 102,
        /// <summary>
        /// 납기회의 데이터가 존재하여 삭제할 수 없습니다.
        /// </summary>
        M_103 = 103,
        /// <summary>
        /// 출고 데이터가 존재하여 삭제할 수 없습니다.
        /// </summary>
        M_104 = 104,
        /// <summary>
        /// 턴키 발주 데이터가 존재하여 삭제할 수 없습니다.
        /// </summary>
        M_105 = 105,
        /// <summary>
        /// 턴키의뢰가 존재하는 항목이 있습니다. 확인 부탁드립니다.
        /// </summary>
        M_106 = 106,
        /// <summary>
        /// 작업의뢰수량이 없는 항목이 존재합니다. 확인 부탁드립니다.
        /// </summary>
        M_107 = 107,
        /// <summary>
        /// 생산계획시작 혹은 종료일이 없습니다. 확인 부탁드립니다.
        /// </summary>
        M_108 = 108,
        /// <summary>
        /// 작업지시가 없는 항목이 존재합니다. 확인 부탁드립니다.
        /// </summary>
        M_109 = 109,
        /// <summary>
        /// 작업이 이미 진행된 작업지시 항목이 존재합니다. 확인 부탁드립니다.
        /// </summary>
        M_110 = 110,
        /// <summary>
        /// 초기 분할을 시키기 위한 데이터이기 때문에 삭제할 수 없습니다.
        /// </summary>
        M_111 = 111,
        /// <summary>
        /// 같은 품목만 추가할 수 있습니다. 확인 부탁드립니다.
        /// </summary>
        M_112 = 112,
        /// <summary>
        /// 임의 포장 작업지시만 삭제가 가능합니다. 확인 부탁드립니다.
        /// </summary>
        M_113 = 113,
        /// <summary>
        /// 반제품 작업지시에 대하여 진행된 항목이 존재합니다. 반제품을 제외하고 삭제하시겠습니까?
        /// </summary>
        M_114 = 114,
        /// <summary>
        /// 출력할 수 있는 실적이 없습니다. 실적 등록 후 출력해 주시기 바랍니다.
        /// </summary>
        M_115 = 115,
        /// <summary>
        /// 해당 설비에 일일점검이 되어있지 않습니다. 확인 부탁드립니다.
        /// </summary>
        M_116 = 116,
        /// <summary>
        /// 입력한 입고 LOT NO의 이전 LOT NO 재고가 남아 있습니다. 확인해 주시기 바랍니다.
        /// </summary>
        M_117 = 117,
        /// <summary>
        /// 이미 분할처리된 항목입니다. 확인 부탁드립니다.
        /// </summary>
        M_118 = 118,
        /// <summary>
        /// 반품이 완료되었습니다. 자재재고관리에서 확인해 주시기 바랍니다.
        /// </summary>
        M_119 = 119,
        /// <summary>
        /// 생산 LOT번호를 새로 생성하시겠습니까?
        /// </summary>
        M_120 = 120,
        /// <summary>
        /// 작업시간등록
        /// </summary>
        M_121 = 121,
        /// <summary>
        /// 생산실적
        /// </summary>
        M_122 = 122,
        /// <summary>
        /// 납품계획삭제불가
        /// </summary>
        M_123 = 123,
        /// <summary>
        /// 포장공정삭제
        /// </summary>
        M_124 = 124,
        /// <summary>
        ///사업자번호
        /// </summary>
        M_125 = 125,
        /// <summary>
        /// {0}를 모두 입력하시기 바랍니다.
        /// </summary>
        M_126 = 126,
        /// <summary>
        ///잘못된 {0} 입니다.
        /// </summary>
        M_127 = 127,
        /// <summary>
        /// 반품처리가 완료되었습니다. 자재재고관리에서 확인해 주시기 바랍니다.
        /// </summary>
        M_128 = 128,
        /// <summary>
        ///현재 투입된 {0} 입니다.
        /// </summary>
        M_129 = 129,
        /// <summary>
        ///{0}을 생성하시겠습니까?
        /// </summary>
        M_130 = 130,
        /// <summary>
        ///선택한 작업지시의 리워크공정 작업을 시작하시겠습니까?
        /// </summary>
        M_131 = 131,
        /// <summary>
        ///리워크 공정은 [부적합관리]에서 생성/삭제하시기 바랍니다.
        /// </summary>
        M_132 = 132,

        /// <summary>
        /// 4단계까지만 가능합니다.
        /// </summary>
        M_133 = 133,
    }
}
